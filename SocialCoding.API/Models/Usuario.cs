namespace SocialCoding.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public byte[] ContraHash { get; set; }
        public byte[] ContraSalt { get; set; }
    }
}