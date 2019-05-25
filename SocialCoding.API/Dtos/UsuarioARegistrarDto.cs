using System.ComponentModel.DataAnnotations;

namespace SocialCoding.API.Dtos {
    public class UsuarioARegistrarDto {
        [Required (ErrorMessage = "Este campo es obligatorio.")]
        [StringLength (20, MinimumLength = 3,
            ErrorMessage = "El nombre de usuario debe estar entre 3 y 20 caracteres")]
        public string NombreUsuario { get; set; }

        [Required (ErrorMessage = "Este campo es obligatorio.")]
        [StringLength (25, MinimumLength = 6,
            ErrorMessage = "La contraseña debe estar entre 6 y 25 caracteres")]
        public string Contra { get; set; }
    }
}