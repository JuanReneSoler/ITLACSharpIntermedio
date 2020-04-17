using System;
using System.ComponentModel.DataAnnotations;


namespace Models.Dtos
{
    public class VotacionRequest
    {
        public int Id { get; set; }

        [Display(Name="Seleccione Ciudadano")]
        public int CiudadanoId { get; set; }

        [Display(Name="Seleccion Eleccion Activa")]
        public int EleccionId { get; set; }

        [Display(Name="Seleccione el candidato de su preferencia.")]
        public int[] CandidatosId { get; set; }
    }
}
