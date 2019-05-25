using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Dtos;
using SocialCoding.API.Models;

namespace SocialCoding.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuth _auth;

        public AuthController (IAuth auth) => _auth = auth;

        [HttpPost ("registrar")]
        public async Task<IActionResult> Registrar (UsuarioARegistrarDto usuarioARegistrarDto) {
            usuarioARegistrarDto.NombreUsuario = usuarioARegistrarDto.NombreUsuario.ToLower ();

            if (await _auth.UsuarioExiste (usuarioARegistrarDto.NombreUsuario))
                return BadRequest ("Usuario ya existe");

            var usuarioACrear = new Usuario {
                NombreUsuario = usuarioARegistrarDto.NombreUsuario
            };

            var usuarioCreado = await _auth.Registrar (usuarioACrear, usuarioARegistrarDto.Contra);

            return StatusCode (201);
        }

    }
}