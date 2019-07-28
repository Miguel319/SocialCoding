using System;
using System.Collections.Generic;

namespace SocialCoding.API.Models {
    public class Usuario {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public byte[] ContraHash { get; set; }
        public byte[] ContraSalt { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Alias { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime UltimaSesion { get; set; }
        public string TrabajaEn { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public ICollection<Imagen> Imagenes { get; set; }
        public string Lenguajes { get; set; }
        public string Hobbies { get; set; }
        public ICollection<MeGusta> MeGustadores { get; set; }
        public ICollection<MeGusta> MeGustas { get; set; }
        public ICollection<Mensaje> MensajesEnviados { get; set; }
        public ICollection<Mensaje> MensajesRecibidos { get; set; }
    }
}