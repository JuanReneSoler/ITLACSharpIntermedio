using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Votos
    {
        public int Id { get; set; }
        public int EleccionId { get; set; }
        public int CandidatoId { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Candidatos Candidato { get; set; }
        public virtual Elecciones Eleccion { get; set; }
    }
}
