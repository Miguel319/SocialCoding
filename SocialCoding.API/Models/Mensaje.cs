using System;
namespace SocialCoding.API.Models
{
    public class Mensaje
    {
        public int Id { get; set; }
        public int RemitenteId { get; set; }
        public Usuario Remitente { get; set; }
        public int ReceptorId { get; set; }
        public Usuario Receptor { get; set; }
        public string Contenido { get; set; }
        public bool Leido { get; set; }
        public DateTime? LeidoEn { get; set; }
        public DateTime MensajeEnviado { get; set; }
        public bool RemitenteLoElimino { get; set; }
        public bool ReceptorLoElimino { get; set; }
    }
}