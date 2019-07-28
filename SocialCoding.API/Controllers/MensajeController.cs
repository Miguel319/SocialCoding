using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialCoding.API.Data;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Dtos;
using SocialCoding.API.Helpers;
using SocialCoding.API.Models;

namespace SocialCoding.API.Controllers {
    [ServiceFilter (typeof (ActividadUsuario))]
    [Authorize]
    [Route ("api/usuarios/{usuarioId}/mensajes")]
    [ApiController]
    public class MensajeController : ControllerBase {
        private readonly ICoderos _coderos;
        private readonly IMapper _mapper;

        public MensajeController (ICoderos coderos, IMapper mapper) {
            _coderos = coderos;
            _mapper = mapper;
        }

        [HttpGet ("{id}", Name = "ObtenerMensaje")]
        public async Task<IActionResult> ObtenerMensaje (int usuarioId, int id) {
            if (usuarioId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value))
                return Unauthorized ();

            var mensajeDelRepo = await _coderos.ObtenerMensaje (id);

            if (mensajeDelRepo == null) return NotFound ();

            return Ok (mensajeDelRepo);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMensajesParaUsuario (int usuarioId, [FromQuery] MensajeParams mensajeParams) {
            if (usuarioId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value))
                return Unauthorized ();

            mensajeParams.UsuarioId = usuarioId;

            var mensajesDelRepo = await _coderos.ObtenerMensajesParaUsuario (mensajeParams);

            var mensajes = _mapper.Map<IEnumerable<MensajeARetornarDto>> (mensajesDelRepo);

            Response.AgregarPaginacion (mensajesDelRepo.PaginaActual, mensajesDelRepo.TamanoPagina,
                mensajesDelRepo.ConteoTotal, mensajesDelRepo.PaginasTotales);

            return Ok (mensajes);
        }

        [HttpPost]
        public async Task<IActionResult> CrearMensaje (int usuarioId, MensajeACrearDto mensajeACrearDto) {
            if (usuarioId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value))
                return Unauthorized ();

            mensajeACrearDto.RemitenteId = usuarioId;

            var receptor = await _coderos.ObtenerUsuario (mensajeACrearDto.ReceptorId);

            if (receptor == null)
                return BadRequest ("No se pudo encontrar al usuario.");

            var mensaje = _mapper.Map<Mensaje> (mensajeACrearDto);

            _coderos.Agregar<Mensaje> (mensaje);

            var mensajeARetornar = _mapper.Map<MensajeACrearDto> (mensaje);

            if (await _coderos.Guardar ())
                return CreatedAtRoute ("ObtenerMensaje",
                    new { id = mensaje.Id }, mensajeARetornar);

            throw new Exception ("Error al enviar el mensaje");
        }

        [HttpGet("conversacion/{receptorId}")]
        public async Task<IActionResult> ObtenerConversacion(int usuarioId, int receptorId){
            if (usuarioId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var mensajesDelRepo = await _coderos.ObtenerConversacion(usuarioId, receptorId);

            var conversacion = _mapper.Map<IEnumerable<MensajeARetornarDto>>(mensajesDelRepo);

            return Ok(conversacion);
        }
    }
}