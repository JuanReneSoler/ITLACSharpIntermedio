using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class LogInRequest
    {
        [DisplayName("Usuario")]
        [Required(ErrorMessage = "El nombre de usuario es incorrecto")]
        public string UserName { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La Contraseña es incorrecta")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Recuerdame")]
        public bool RememberMe { get; set; }
    }
}
