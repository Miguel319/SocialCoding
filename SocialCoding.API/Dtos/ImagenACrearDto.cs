using System;
using Microsoft.AspNetCore.Http;

namespace SocialCoding.API.Dtos
{
    public class ImagenACrearDto
    {
        public string Url { get; set; }
        public IFormFile Archivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime AgregadaEn { get; set; }
        public string IdPublica { get; set; }

        public ImagenACrearDto()
        {
            AgregadaEn = DateTime.Now;
        }
    }
}