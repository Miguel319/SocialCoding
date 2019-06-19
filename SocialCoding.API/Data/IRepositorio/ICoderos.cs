using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data.IRepositorio
{
    public interface ICoderos
    {
         void Agregar<T>(T entidad) where T: class;
         void Eliminar<T>(T entidad) where T: class;
         Task<bool> Guardar();
         Task<IEnumerable<Usuario>> ObtenerUsuarios();
         Task<Usuario> ObtenerUsuario(int id);
         Task<Imagen> ObtenerImagen(int id);
         Task<Imagen> ObtenerFotoDePerfil(int usuarioId);
    }
}