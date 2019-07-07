using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Helpers;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data.LogicaNegocios {
    public class Coderos : BaseContexto, ICoderos {
        public Coderos (SocialCodingContext contexto) : base (contexto) { }

        public void Agregar<T> (T entidad) where T : class => _contexto.Add (entidad);

        public void Eliminar<T> (T entidad) where T : class => _contexto.Remove (entidad);

        public async Task<bool> Guardar () => await _contexto.SaveChangesAsync () > 0;

        public async Task<Imagen> ObtenerFotoDePerfil (int usuarioId) => await _contexto.Imagenes.Where (img => img.UsuarioId == usuarioId)
            .FirstOrDefaultAsync (img => img.DePerfil);

        public async Task<Imagen> ObtenerImagen (int id) => await _contexto.Imagenes.FirstOrDefaultAsync (img => img.Id == id);

        public async Task<Usuario> ObtenerUsuario (int id) => await _contexto.Usuarios.Include (img => img.Imagenes)
            .FirstOrDefaultAsync (usuario => usuario.Id == id);

        public async Task<ListaPaginada<Usuario>> ObtenerUsuarios (UsuarioParams usuarioParams) {
            var usuarios = _contexto.Usuarios.Include (img => img.Imagenes);
            return await ListaPaginada<Usuario>.Crear (usuarios,
                usuarioParams.NoPagina, usuarioParams.TamanoPagina);
        }

    }
}