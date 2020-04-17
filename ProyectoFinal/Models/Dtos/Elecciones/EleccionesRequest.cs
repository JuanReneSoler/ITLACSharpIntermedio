using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class EleccionesRequest
    {
        public int Id { get; set; }

        [Display(Name="Descripcion de la eleccion")]
        [Required(ErrorMessage="Campo nombre es obligaorio.")]
        public string Nombre { get; set; }

        [Display(Name="Fecha")]
        [Required(ErrorMessage="Fecha es obligatoria.")]
        public System.DateTime Fecha { get; set; }
    }
}