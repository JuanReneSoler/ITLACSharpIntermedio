
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class AdministradoresRequest
    {
        public string Id { get; set; }
        public string SecurityStamp { get; set; }

        [Display(Name="Correo")]
        [DataType(DataType.EmailAddress, ErrorMessage="Aqui debe poner su Direccion de Correo.")]
        [EmailAddress(ErrorMessage="Correo invalido.")]
        [Required(ErrorMessage="La direccion de correo es requerida.")]
        public string Email { get; set; }

        [Display(Name="Nombre de Usuario")]
        [Required(ErrorMessage="El nombre de usuario es requerido.")]
        public string UserName { get; set; }

        [Display(Name="Contrasena")]
        [DataType(DataType.Password, ErrorMessage="Contrasena invalida.")]
        [Required(ErrorMessage="Debe ingresar su contrasena valida")]
        public string Password { get; set; }

        [Display(Name="Confirme Contrasena")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage="Las contrasenas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public bool Estado { get; set; }
    }
}