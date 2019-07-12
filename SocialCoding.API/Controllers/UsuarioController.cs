using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Dtos;
using SocialCoding.API.Helpers;
using SocialCoding.API.Models;

namespace SocialCoding.API.Controllers {
    [ServiceFilter (typeof (ActividadUsuario))]
    [Authorize]
    [Route ("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        private readonly ICoderos _coderos;
        private readonly IMapper _mapper;

        public UsuarioController (ICoderos coderos, IMapper mapper) {
            _coderos = coderos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios ([FromQuery] UsuarioParams usuarioParams) {
            var usuarioActualId = int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value);

            var usuarioDelRepo = await _coderos.ObtenerUsuario(usuarioActualId);

            usuarioParams.UsuarioId = usuarioActualId;

            if (string.IsNullOrEmpty(usuarioParams.Genero)) {
                usuarioParams.Genero = usuarioDelRepo.Genero == "masculino" ?  "Femenino" : "masculino";
            }

            var usuarios = await _coderos.ObtenerUsuarios (usuarioParams);

            var usuariosARetornar = _mapper.Map<IEnumerable<UsuarioListaDto>> (usuarios);

            Response.AgregarPaginacion (usuarios.PaginaActual, usuarios.TamanoPagina, usuarios.ConteoTotal,
                usuarios.PaginasTotales);

            return Ok (usuariosARetornar);
        }

        [HttpGet ("{id}", Name = "ObtenerUsuario")]
        public async Task<IActionResult> ObtenerUsuario (int id) => Ok (_mapper.Map<UsuarioDetallesDto> (await _coderos.ObtenerUsuario (id)));

        [HttpPut ("{id}")]
        public async Task<IActionResult> ActualizarUsuario (int id, UsuarioEdicionDto usuarioEdicionDto) {
            if (id != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) return Unauthorized ();

            var usuarioRepo = await _coderos.ObtenerUsuario (id);

            _mapper.Map (usuarioEdicionDto, usuarioRepo);

            if (await _coderos.Guardar ()) return NoContent ();

            throw new Exception ($"Error al actualizar usuario {id}");
        }

        [HttpPost ("{id}/meGusta/{recibidorId}")]
        public async Task<IActionResult> MeGustaUsuario (int id, int recibidorId) {
            if (id != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value))
                return Unauthorized ();

            var meGusta = await _coderos.ObtenerMeGusta (id, recibidorId);

            if (meGusta != null)
                return BadRequest ($"No puede darle {"Me gusta "} al mismo usuario más de una vez.");

            if (await _coderos.ObtenerUsuario (recibidorId) == null)
                return NotFound ("Usuario no encontrado");

            meGusta = new MeGusta {
                MeGustadorId = id,
                MeGustaaId = recibidorId
            };

            _coderos.Agregar<MeGusta> (meGusta);

            if (await _coderos.Guardar ()) return Ok ();

            return BadRequest ($"Error al darle {"'Me Gusta'"} al usuario");
        }
    }
}