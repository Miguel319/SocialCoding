using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialCoding.API.Data.IRepositorio;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data.LogicaNegocios {
    public class Auth : BaseContexto, IAuth {
        public Auth (SocialCodingContext contexto) : base (contexto) { }

        public async Task<Usuario> IniciarSesion (string nombreUsuario, string contra) 
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == nombreUsuario);

            if (usuario == null) return null;

            if (!VerificarContraHash (contra,  usuario.ContraHash, usuario.ContraSalt))
                return null;

            return usuario;
        }

        private bool VerificarContraHash (string contra, byte[] contraHash, byte[] contraSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 (contraSalt)) {
                var hashCalculado = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (contra));

                for (int i = 0; i < hashCalculado.Length; i++) {
                    if (hashCalculado[i] != contraHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<Usuario> Registrar (Usuario usuario, string contra) 
        {
            byte[] contraHash, contraSalt;

            CrearContraHash (contra, out contraHash, out contraSalt);
            usuario.ContraHash = contraHash;
            usuario.ContraSalt = contraSalt;

            await _contexto.Usuarios.AddAsync (usuario);
            await _contexto.SaveChangesAsync ();

            return usuario;
        }

        public async Task<bool> UsuarioExiste (string nombreUsuario) =>
            await _contexto.Usuarios.CountAsync (x => x.NombreUsuario == nombreUsuario) > 0;

        private void CrearContraHash (string contra, out byte[] contraHash, out byte[] contraSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                contraSalt = hmac.Key;
                contraHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (contra));
            }

        }
    }
}