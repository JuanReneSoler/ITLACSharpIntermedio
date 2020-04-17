using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Models.Dtos
{
    public partial class PartidoRequest
    {
        public int Id { get; set; }

        [DisplayName("Nombre del Partido")]
        [Required(ErrorMessage="Campo nombre es requerido.")]
        public string Nombre { get; set; }

        [DisplayName("Descripcion")]
        [Required(ErrorMessage="Campo descripcion es requerido.")]
        public string Descripcion { get; set; }

        [DisplayName("Solo archivos tipo: jpg, jpeg y png")]
        public IFormFile Logo { get; set; }

        public string LogoBase64 { get; set; }
        public string OldPath { get; set; }
    }
}