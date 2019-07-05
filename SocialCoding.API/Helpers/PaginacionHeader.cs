namespace SocialCoding.API.Helpers {
    public class PaginacionHeader {
        public int PaginaActual { get; set; }
        public int ElementosPorPagina { get; set; }
        public int ElementosTotales { get; set; }
        public int PaginasTotales { get; set; }
        
        public PaginacionHeader(int paginaActual, 
        int elementosPorPagina, int elementosTotales, int paginasTotales)
        {
            this.PaginaActual = paginaActual;
            this.ElementosPorPagina = elementosPorPagina;
            this.ElementosTotales = elementosTotales;
            this.PaginasTotales = paginasTotales;
        }
    }
}