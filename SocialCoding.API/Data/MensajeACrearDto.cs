using System;
namespace SocialCoding.API.Data {
    public class MensajeACrearDto {
        public int RemitenteId { get; set; }
        public int ReceptorId { get; set; }
        public DateTime MensajeEnviado { get; set; }
        public string Contenido { get; set; }
        public MensajeACrearDto () => MensajeEnviado = DateTime.Now;
    }
}