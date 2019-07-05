using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SocialCoding.API.Helpers {
    public static class Extensiones {
        public static void AgregarError (this HttpResponse respuesta, string mensaje) {
            respuesta.Headers.Add ("Application-Error", mensaje);
            respuesta.Headers.Add ("Access-Control-Expose-Headers", "Application-Error");
            respuesta.Headers.Add ("Access-Control-Allow-Origin", "*");
        }

        public static void AgregarPaginacion (this HttpResponse respuesta,
            int paginaActual, int elementosPorPagina, int elementosTotales,
            int paginasTotales) {
            var paginacionHeader = new PaginacionHeader (paginaActual, elementosPorPagina,
                elementosTotales, paginasTotales);

            var camelCase = new JsonSerializerSettings();
            camelCase.ContractResolver = new CamelCasePropertyNamesContractResolver();
            respuesta.Headers.Add ("Pagination", JsonConvert.SerializeObject (paginacionHeader, camelCase));
            respuesta.Headers.Add ("Access-Control-Expose-Headers", "Pagination");

        }

        public static int CalcularEdad (this DateTime fecha) {
            var edad = DateTime.Today.Year - fecha.Year;

            if (fecha.AddYears (edad) > DateTime.Today)
                edad--;

            return edad;
        }
    }
}