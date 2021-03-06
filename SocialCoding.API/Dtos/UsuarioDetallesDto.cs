using System.Collections;
using System;
using SocialCoding.API.Models;
using System.Collections.Generic;

namespace SocialCoding.API.Dtos {
    public class UsuarioDetallesDto {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string Alias { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime UltimaSesion { get; set; }
        public string TrabajaEn { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Lenguajes { get; set; }
        public string Hobbies { get; set; }
        public string ImagenUrl { get; set; }
        public ICollection<ImagenDetallesDto> Imagenes { get; set; }
    }
}