using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data.LogicaNegocios
{
    public class Coderos : BaseContexto, ICoderos
    {
        public Coderos(SocialCodingContext contexto) : base(contexto) {}

        public void Agregar<T>(T entidad) where T : class
          =>  _contexto.Add(entidad);

        public void Eliminar<T>(T entidad) where T : class 
         =>  _contexto.Remove(entidad);

        public async Task<bool> Guardar()
            => await _contexto.SaveChangesAsync() > 0;

        public async Task<Imagen> ObtenerImagen(int id)
           => await _contexto.Imagenes.FirstOrDefaultAsync(img => img.Id == id);

        public async Task<Usuario> ObtenerUsuario(int id)
         => await _contexto.Usuarios.Include(img => img.Imagenes)
                .FirstOrDefaultAsync(usuario => usuario.Id == id);

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
            => await _contexto.Usuarios.Include(img => img.Imagenes)
                .ToListAsync();
    }
}