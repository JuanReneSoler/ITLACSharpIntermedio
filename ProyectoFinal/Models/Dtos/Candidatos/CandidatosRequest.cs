using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Models.Dtos
{
    public class CandidatosRequest
    {
        public int Id { get; set; }

        public int EleccionId { get; set; }

        [DisplayName("Ciudadano")]
        [Required(ErrorMessage="Debe seleccionar un ciudadano")]
        public int CiudadanoId { get; set; }

        public string CiudadanoText { get; set; }

        [DisplayName("Partido politico")]
        [Required(ErrorMessage="Debe seleccionar un partido")]
        public int PartidoId { get; set; }

        public string PartidoText { get; set; }

        [DisplayName("Puesto")]
        [Required(ErrorMessage="Debe Seleccionar un puesto para la candidatura")]
        public int PuestoId { get; set; }

        public string PuestoText { get; set; }

        [DisplayName("Foto de Perfil")]
        public IFormFile Foto { get; set; }

        public string FotoBase64 {get; set;}

        public string OldPath { get; set; }
    }
}
