using System.ComponentModel.DataAnnotations;

namespace Tareas.Models
{
    public class RegisterDto
    {
        [Display(Name="Correo")]
        [DataType(DataType.EmailAddress, ErrorMessage="Email no valido")]
        [Required(ErrorMessage="Email es requerido")]
        public string Email { get; set; }

        [Display(Name="Usuario")]
        [Required(ErrorMessage="Usuario requerido")]
        public string User { get; set; }

        [Display(Name="Contrasena")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage="Contrasena es requerida")]
        public string Password { get; set; }

        [Display(Name="Confirme Contrasena")]
        [Compare(nameof(Password))]
        [Required(ErrorMessage="Las contrasenas no machea")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

