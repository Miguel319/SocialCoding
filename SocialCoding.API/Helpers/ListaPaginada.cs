using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialCoding.API.Helpers {
    public class ListaPaginada<T> : List<T> {
        public int PaginaActual { get; set; }
        public int PaginasTotales { get; set; }
        public int TamanoPagina { get; set; }
        public int ConteoTotal { get; set; }

        public ListaPaginada (List<T> elementos, int conteo, int noPagina, int tamanoPag) {
            ConteoTotal = conteo;
            TamanoPagina = tamanoPag;
            PaginaActual = noPagina;
            PaginasTotales = (int) Math.Ceiling (conteo / (double) TamanoPagina); 
            this.AddRange(elementos);
        }

        public static async Task<ListaPaginada<T>> Crear(IQueryable<T> fuente,
            int noPagina, int tamanoPag) {
                var conteo = await fuente.CountAsync();
                var elementos = await fuente.Skip((noPagina - 1) * tamanoPag).Take(tamanoPag).ToListAsync();
                return new ListaPaginada<T>(elementos, conteo, noPagina, tamanoPag);
            }
    }
}