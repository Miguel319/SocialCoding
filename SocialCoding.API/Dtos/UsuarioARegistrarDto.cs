using System;
using System.ComponentModel.DataAnnotations;

namespace SocialCoding.API.Dtos {
    public class UsuarioARegistrarDto {
        [Required (ErrorMessage = "Este campo es obligatorio.")]
        [StringLength (20, MinimumLength = 3,
            ErrorMessage = "El nombre de usuario debe estar entre 3 y 20 caracteres")]
        public string NombreUsuario { get; set; }

        [Required (ErrorMessage = "Este campo es obligatorio.")]
        [StringLength (25, MinimumLength = 4,
            ErrorMessage = "La contraseña debe estar entre 4 y 25 caracteres")]
        public string Contra { get; set; }

        [Required]
        public string Alias { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Ciudad { get; set; }

        [Required]
        public string Pais { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime UltimaSesion { get; set; }

        public UsuarioARegistrarDto () {
            CreadoEn = DateTime.Now;
            UltimaSesion = DateTime.Now;
        }
    }
}