using System.ComponentModel.DataAnnotations;

namespace Tareas.Models
{
    public class LogInDto
    {
        [Display(Name="Usuario")]
        [Required(ErrorMessage="Usuario es requerido")]
        public string UserName { get; set; }

        [Display(Name="Contrasena")]
        [Required(ErrorMessage="Contrasena es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Recuerdame")]
        public bool RememberMe { get; set; }
    }
}