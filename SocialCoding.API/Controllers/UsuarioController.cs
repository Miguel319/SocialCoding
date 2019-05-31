using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Dtos;
using SocialCoding.API.Models;

namespace SocialCoding.API.Controllers {
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
        public async Task<IActionResult> ObtenerUsuarios () 
        => Ok (_mapper.Map<IEnumerable<UsuarioListaDto>> (await _coderos.ObtenerUsuarios ()));

        [HttpGet ("{id}")]
        public async Task<IActionResult> ObtenerUsuario (int id) 
        => Ok (_mapper.Map<UsuarioDetallesDto> (await _coderos.ObtenerUsuario (id)));

        // public async Task<IActionResult> Agregar(Usuario usuario) {

        // }
    }
}