
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class PuestoRequest
    {
        public int Id { get; set; }

        [DisplayName("Nombre del puesto")]
        [Required(ErrorMessage="Debe especificar un nombre al puesto")]
        public string Nombre { get; set; }

        [DisplayName("Descripcion")]
        [Required(ErrorMessage="Este campo es obligatorio.")]
        public string Descripcion { get; set; }
    }
}