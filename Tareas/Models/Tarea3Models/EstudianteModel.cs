using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tareas.Models
{
    public class EstudianteModel
    {
        public int id { get; set; }

        [DisplayName("Nombre del Estudiante:")]
        [Required(ErrorMessage="Debe especificar un Nombre.")]
        public string nombre { get; set; }

        [DisplayName("Apellido del Estudiante:")]
        [Required(ErrorMessage="Debe especificar un Apellido.")]
        public string apellido { get; set; }

        [DisplayName("Carrera Actual")]
        [Required(ErrorMessage="Debe seleccionar una Carrera.")]
        public int carreraId { get; set; }

        [DisplayName("Carrera Actual")]
        public string carreraText { get; set; }

        [DisplayName("Estado")]
        public bool estado { get; set; }
    }
}

