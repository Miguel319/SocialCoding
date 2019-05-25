using System.Threading.Tasks;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data.IRepositorio {
    public interface IAuth {
        Task<Usuario> Registrar (Usuario usuario, string contra);
        Task<Usuario> IniciarSesion (string nombreUsuario, string contra);
        Task<bool> UsuarioExiste (string nombreUsuario);
    }
}