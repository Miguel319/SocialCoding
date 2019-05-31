using System;
using Microsoft.AspNetCore.Http;

namespace SocialCoding.API.Helpers {
    public static class Extensiones {
        public static void AgregarError (this HttpResponse respuesta, string mensaje) {
            respuesta.Headers.Add("Application-Error", mensaje);
            respuesta.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            respuesta.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalcularEdad(this DateTime fecha) {
            var edad = DateTime.Today.Year - fecha.Year;

            if ( fecha.AddYears(edad) > DateTime.Today ) 
                edad --;
            
            return edad;
        }
    }
}