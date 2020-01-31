using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Tarea1.Models
{
    public class p1Request{

        [Required()]
        [DisplayName("Fecha de Nacimiento")]
        [DataType(DataType.DateTime)]
        public System.DateTime fechaNacimiento { get; set; }

        public string Result { get; set; }
    }

    public class p2Request{

        [Required]
        [DisplayName("A")]
        public decimal a { get; set; }

        [Required]
        [DisplayName("B")]
        public decimal b { get; set; }

        [Required]
        [DisplayName("C")]
        public decimal c { get; set; }
    }
}









