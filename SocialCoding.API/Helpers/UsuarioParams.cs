namespace SocialCoding.API.Helpers {
    public class UsuarioParams {
        private const int MaxPaginas = 50;
        public int NoPagina { get; set; } = 1;
        private int tamanoPagina = 10;
        public int TamanoPagina {
            get { return tamanoPagina; }
            set { tamanoPagina = (value > MaxPaginas) ? MaxPaginas : value; }
        }

        public int UsuarioId { get; set; }
        public string Genero { get; set; }
        public bool MeGustas { get; set; } = false;
        public bool MeGustadores { get; set; } = false;

    }
}