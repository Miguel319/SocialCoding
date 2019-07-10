using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialCoding.API.Helpers;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data.IRepositorio
{
    public interface ICoderos
    {
         void Agregar<T>(T entidad) where T: class;
         void Eliminar<T>(T entidad) where T: class;
         Task<bool> Guardar();
         Task<ListaPaginada<Usuario>> ObtenerUsuarios(UsuarioParams usuarioParams);
         Task<Usuario> ObtenerUsuario(int id);
         Task<Imagen> ObtenerImagen(int id);
         Task<Imagen> ObtenerFotoDePerfil(int usuarioId);
         Task<MeGusta> ObtenerMeGusta(int usuarioId, int recibidorId);
    }
}