namespace SocialCoding.API.Models {
    public class MeGusta {
        public int MeGustadorId { get; set; }
        public int MeGustaaId { get; set; }
        public Usuario MeGustador { get; set; }
        public Usuario MeGustaa { get; set; }
    }
}