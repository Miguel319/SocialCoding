using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocialCoding.API.Data.LogicaNegocios;
using SocialCoding.API.Models;

namespace SocialCoding.API.Data {
    public class Seed : BaseContexto {
        public Seed (SocialCodingContext contexto) : base (contexto) { }

        public void seedUsuarios () {
            var datosUsuarios = System.IO.File.ReadAllText ("Data/coolSeed.json");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>> (datosUsuarios);

            foreach (var usuario in usuarios) {
                byte[] contraHash, contraSalt;
                CrearContraHash ("helloworld12", out contraHash, out contraSalt);

                usuario.ContraHash = contraHash;
                usuario.ContraSalt = contraSalt;
                usuario.NombreUsuario = usuario.NombreUsuario.ToLower ();

                _contexto.Usuarios.AddAsync (usuario);
            }

            _contexto.SaveChangesAsync();
        }

        private void CrearContraHash (string contra, out byte[] contraHash, out byte[] contraSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                contraSalt = hmac.Key;
                contraHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (contra));
            }

        }
    }
}