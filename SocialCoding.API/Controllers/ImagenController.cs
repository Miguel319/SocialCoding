using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Dtos;
using SocialCoding.API.Helpers;
using SocialCoding.API.Models;

namespace SocialCoding.API.Controllers {
    [Authorize]
    [Route ("api/usuarios/{usuarioId}/imagenes")]
    [ApiController]
    public class ImagenController : ControllerBase {
        private readonly ICoderos _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary cloudinary;

        public ImagenController (ICoderos repo,
            IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig) {
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            _repo = repo;

            Account acc = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            cloudinary = new Cloudinary (acc);
        }

        [HttpGet ("{id}", Name = "ObtenerImagen")]
        public async Task<IActionResult> ObtenerImagen (int id) {
            var imagenDelRepo = await _repo.ObtenerImagen (id);
            var imagen = _mapper.Map<ImagenARetornarDto> (imagenDelRepo);

            return Ok (imagen);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarImagen (int usuarioId, [FromForm] ImagenACrearDto imagenACrearDto) {

            if (usuarioId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) return Unauthorized ();

            var usuarioDeRepo = await _repo.ObtenerUsuario (usuarioId);

            var file = imagenACrearDto.File;
            var subidaResultados = new ImageUploadResult ();

            if (file.Length > 0) {
                using (var stream = file.OpenReadStream ()) {
                    var parametrosDeSubida = new ImageUploadParams () {
                    File = new FileDescription (file.Name, stream),
                    Transformation = new Transformation ().Width (500).Height (500)
                    .Crop ("fill").Gravity ("face")
                    };

                    subidaResultados = cloudinary.Upload (parametrosDeSubida);
                }
            }

            imagenACrearDto.Url = subidaResultados.Uri.ToString ();
            imagenACrearDto.IdPublica = subidaResultados.PublicId;

            var imagen = _mapper.Map<Imagen> (imagenACrearDto);

            if (!usuarioDeRepo.Imagenes.Any (img => img.DePerfil)) imagen.DePerfil = true;

            usuarioDeRepo.Imagenes.Add (imagen);

            if (await _repo.Guardar ()) {
                var ImagenARetornarDto = _mapper.Map<ImagenARetornarDto> (imagen);
                return CreatedAtRoute ("ObtenerImagen",
                    new { id = imagen.Id }, ImagenARetornarDto);

            }

            return BadRequest ("No se pudo añadir la imagen");
        }
    }
}