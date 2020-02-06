using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tareas.Models
{
    public class ListaSignosModel{
        public string Nombre { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }
    }

    public class p1Request
    {
        [Required]
        [DisplayName("Fecha de Nacimiento")]
        public System.DateTime fechaNacimiento { get; set; }

        [DisplayName("Resultado:")]
        public string resultado { get; set; }
    }

    public class p2Request{
        [Required]
        [DisplayName("Valor de A:")]
        public double a { get; set; }
        
        [Required]
        [DisplayName("Valor de B:")]
        public double b { get; set; }

        [Required]
        [DisplayName("Valor de C:")]
        public double c { get; set; }

        [DisplayName("Resultado:")]
        public string resultado { get; set; }
    }
}

