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

        /*     public async Task<MeGusta> ObtenerMeGusta (int usuarioId, int recibidorId) =>
            await _contexto.MeGustas.FirstOrDefaultAsync (x => x.MeGustadorId == usuarioId && x.MeGustaaId == recibidorId);
*/

        public async Task<MeGusta> ObtenerMeGusta (int usuarioId, int recibidorId) => await _contexto.MeGustas.FirstOrDefaultAsync (x => x.MeGustadorId == usuarioId && x.MeGustaaId == recibidorId);

        public async Task<Usuario> ObtenerUsuario (int id) => await _contexto.Usuarios
            .Include (img => img.Imagenes)
            .FirstOrDefaultAsync (usuario => usuario.Id == id);

        public async Task<ListaPaginada<Usuario>> ObtenerUsuarios (UsuarioParams usuarioParams) {
            var usuarios = _contexto.Usuarios.Include (img => img.Imagenes).AsQueryable ();

            if (usuarioParams.MeGustadores) {
                /*var meGustadores = await ObtenerMeGustadoresDelUsuario (usuarioParams.UsuarioId);
                usuarios = usuarios.Where (x => x.MeGustadores.Any (y => y.MeGustadorId == x.Id));*/

                var usuarioMeGustadores = await ObtenerMeGustasDelUsuario (usuarioParams.UsuarioId, usuarioParams.MeGustadores);
                usuarios = usuarios.Where (x => usuarioMeGustadores.Contains (x.Id));
            }

            if (usuarioParams.MeGustas) {
                /*var usuarioMeGustas = await ObtenerMeGustasDelUsuario (usuarioParams.UsuarioId);
                usuarios = usuarios.Where (x => x.MeGustas.Any (y => y.MeGustadorId == x.Id));*/

                var usuarioMeGustas = await ObtenerMeGustasDelUsuario (usuarioParams.UsuarioId, usuarioParams.MeGustadores);
                usuarios = usuarios.Where (x => usuarioMeGustas.Contains (x.Id));
            }

            return await ListaPaginada<Usuario>.Crear (usuarios,
                usuarioParams.NoPagina, usuarioParams.TamanoPagina);
        }

        private async Task<IEnumerable<int>> ObtenerMeGustasDelUsuario (int id, bool meGustadores) {
            var usuario = await _contexto.Usuarios
                .Include (x => x.MeGustadores)
                .Include (x => x.MeGustas)
                .FirstOrDefaultAsync (x => x.Id == id);

            if (meGustadores)
                return usuario.MeGustadores.Where (x => x.MeGustaaId == id)
                    .Select (x => x.MeGustadorId);

            return usuario.MeGustas.Where (x => x.MeGustadorId == id).Select (x => x.MeGustaaId);
        }
    }
}