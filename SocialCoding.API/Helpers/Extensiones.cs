using Microsoft.AspNetCore.Http;

namespace SocialCoding.API.Helpers {
    public static class Extensiones {
        public static void AgregarError (this HttpResponse respuesta, string mensaje) {
            respuesta.Headers.Add("Application-Error", mensaje);
            respuesta.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            respuesta.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}