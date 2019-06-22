using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthController (IAuth auth, IConfiguration config, IMapper mapper) {
            _config = config;
            _auth = auth;
            _mapper = mapper;
        }

        [HttpPost ("registrar")]
        public async Task<IActionResult> Registrar (UsuarioARegistrarDto usuarioARegistrarDto) {
            usuarioARegistrarDto.NombreUsuario = usuarioARegistrarDto.NombreUsuario.ToLower ();

            if (await _auth.UsuarioExiste (usuarioARegistrarDto.NombreUsuario))
                return BadRequest ("Usuario ya existe");

            var usuarioACrear = _mapper.Map<Usuario> (usuarioARegistrarDto);

            var usuarioCreado = await _auth.Registrar (usuarioACrear, usuarioARegistrarDto.Contra);

            var usuarioARetornar = _mapper.Map<UsuarioDetallesDto> (usuarioCreado);

            return CreatedAtRoute ("ObtenerUsuario", new { controller = "Usuario", id = usuarioCreado.Id }, usuarioARetornar);
        }

        [HttpPost ("isesion")]
        public async Task<IActionResult> ISesion (UsuarioAIniciarSesionDto usuarioARegistrarDto) {
            var usuarioDelRepo = await _auth.IniciarSesion (usuarioARegistrarDto.NombreUsuario.ToLower (), usuarioARegistrarDto.Contra);

            if (usuarioDelRepo == null) return Unauthorized ();

            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, usuarioDelRepo.Id.ToString ()),
                new Claim (ClaimTypes.Name, usuarioDelRepo.NombreUsuario)
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config.GetSection ("AppSettings:Token").Value));

            var credenciales = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = credenciales
            };

            var tokenM = new JwtSecurityTokenHandler ();

            var token = tokenM.CreateToken (tokenDescriptor);

            var usuario = _mapper.Map<UsuarioListaDto> (usuarioDelRepo);

            return Ok (new {
                token = tokenM.WriteToken (token),
                    usuario
            });
        }
    }
}