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
    }
}