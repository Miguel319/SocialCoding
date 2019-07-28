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

        public Task<IEnumerable<Mensaje>> ObtenerConversacion (int usuarioId, int receptorId) {
            throw new System.NotImplementedException ();
        }

        public async Task<Imagen> ObtenerFotoDePerfil (int usuarioId) => await _contexto.Imagenes.Where (img => img.UsuarioId == usuarioId)
            .FirstOrDefaultAsync (img => img.DePerfil);

        public async Task<Imagen> ObtenerImagen (int id) => await _contexto.Imagenes.FirstOrDefaultAsync (img => img.Id == id);

        public async Task<MeGusta> ObtenerMeGusta (int usuarioId, int recibidorId) => 
        await _contexto.MeGustas.FirstOrDefaultAsync (x => x.MeGustadorId == usuarioId && x.MeGustaaId == recibidorId);

        public async Task<Mensaje> ObtenerMensaje (int id) => await _contexto.Mensajes.FirstOrDefaultAsync (x => x.Id == id);

        public async Task<ListaPaginada<Mensaje>> ObtenerMensajesParaUsuario (MensajeParams mensajeParams) {
            var mensajes = _contexto.Mensajes
                .Include (x => x.Remitente).ThenInclude (x => x.Imagenes)
                .Include (x => x.Receptor).ThenInclude (x => x.Imagenes)
                .AsQueryable ();

            switch (mensajeParams.ContenedorMensaje) {
                case "Recibidos":
                    mensajes = mensajes.Where (x => x.ReceptorId == mensajeParams.UsuarioId);
                    break;
                case "Enviados":
                    mensajes = mensajes.Where (x => x.RemitenteId == mensajeParams.UsuarioId);
                    break;
                default:
                    mensajes = mensajes.Where (x => x.ReceptorId == mensajeParams.UsuarioId && !x.Leido);
                    break;
            }

            mensajes = mensajes.OrderByDescending (x => x.MensajeEnviado);
            return await ListaPaginada<Mensaje>.Crear (mensajes, mensajeParams.NoPagina,
                mensajeParams.TamanoPagina);
        }

        public async Task<Usuario> ObtenerUsuario (int id) => await _contexto.Usuarios
            .Include (img => img.Imagenes)
            .FirstOrDefaultAsync (usuario => usuario.Id == id);

        public async Task<ListaPaginada<Usuario>> ObtenerUsuarios (UsuarioParams usuarioParams) {
            var usuarios = _contexto.Usuarios.Include (img => img.Imagenes).AsQueryable ();

            if (usuarioParams.MeGustadores) {
                var usuarioMeGustadores = await ObtenerMeGustasDelUsuario (usuarioParams.UsuarioId, usuarioParams.MeGustadores);
                usuarios = usuarios.Where (x => usuarioMeGustadores.Contains (x.Id));
            }

            if (usuarioParams.MeGustas) {
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