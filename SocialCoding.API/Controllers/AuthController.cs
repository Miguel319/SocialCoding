using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Dtos;
using SocialCoding.API.Models;

namespace SocialCoding.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuth _auth;
        private readonly IConfiguration _config;

        public AuthController (IAuth auth, IConfiguration config) {
            _config = config;
            _auth = auth;
        }

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

        [HttpPost ("isesion")]
        public async Task<IActionResult> ISesion (UsuarioAIniciarSesionDto usuarioARegistrarDto) {
            var usuario = await _auth.IniciarSesion (usuarioARegistrarDto.NombreUsuario.ToLower(), usuarioARegistrarDto.Contra);

            if (usuario == null) return Unauthorized ();

            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, usuario.Id.ToString ()),
                new Claim (ClaimTypes.Name, usuario.NombreUsuario)
            };

            var key = new SymmetricSecurityKey 
            (Encoding.UTF8.GetBytes (_config.GetSection("AppSettings:Token").Value));

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenM = new JwtSecurityTokenHandler();

            var token = tokenM.CreateToken(tokenDescriptor);

            return Ok(new{
                token = tokenM.WriteToken(token)
            });
        }
        
    }

}