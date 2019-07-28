using System;
using SocialCoding.API.Models;

namespace SocialCoding.API.Dtos {
    public class MensajeARetornarDto {
        public int Id { get; set; }
        public int RemitenteId { get; set; }
        public string RemitenteAlias { get; set; }
        public string RemitenteImagenUrl { get; set; }
        public int ReceptorId { get; set; }
        public string ReceptorAlias { get; set; }
        public string ReceptorImagenUrl { get; set; }
        public string Contenido { get; set; }
        public bool Leido { get; set; }
        public DateTime? LeidoEn { get; set; }
        public DateTime MensajeEnviado { get; set; }
    }
}