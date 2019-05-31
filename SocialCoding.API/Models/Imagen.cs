using System;
namespace SocialCoding.API.Models
{
    public class Imagen
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Descripcion { get; set; }
        public DateTime AgregadaEn { get; set; }
        public bool DePerfil { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
    }
}