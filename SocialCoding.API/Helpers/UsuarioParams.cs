namespace SocialCoding.API.Helpers {
    public class UsuarioParams {
        private const int MaxPaginas = 50;
        public int NoPagina { get; set; } = 1;
        private int tamanoPagina = 10;
        public int TamanoPagina {
            get { return tamanoPagina; }
            set { tamanoPagina = (value > MaxPaginas) ? MaxPaginas : value; }
        }

    }
}