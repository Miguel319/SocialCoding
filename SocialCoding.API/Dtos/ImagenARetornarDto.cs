using System;

namespace SocialCoding.API.Dtos {
    public class ImagenARetornarDto {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Descripcion { get; set; }
        public DateTime AgregadaEn { get; set; }
        public bool DePerfil { get; set; }
        public string IdPublica { get; set; }
    }
}