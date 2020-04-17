using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Votantes
    {
        public int Id { get; set; }
        public int CiudadanoId { get; set; }
        public int EleccionId { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Ciudadanos Ciudadano { get; set; }
        public virtual Elecciones Eleccion { get; set; }
    }
}
