using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public partial class CiudadanoRequest
    {
        public int Id { get; set; }

        [DisplayName("Documento de Identidad: ")]
        [StringLength(13)]
        [Required(ErrorMessage="Documento de identidad es requerido.")]
        public string DocIdentidad { get; set; }

        [DisplayName("Nombre del Cuidadano: ")]
        [Required(ErrorMessage="El campo nombre es obligatorio.")]
        public string Nombre { get; set; }

        [DisplayName("Apellido del Ciudadano: ")]
        [Required(ErrorMessage="El campo apellido es obligatorio.")]
        public string Apellido { get; set; }

        [DisplayName("Direccion de Correo Elecctronico: ")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo Invalido.")]
        [EmailAddress(ErrorMessage="LA direccio nde correo no es valida.")]
        public string Email { get; set; }
    }
}