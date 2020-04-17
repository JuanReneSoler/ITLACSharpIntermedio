using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Candidatos
    {
        public Candidatos()
        {
            Votos = new HashSet<Votos>();
        }

        public int Id { get; set; }
        public int CiudadanoId { get; set; }
        public int? PartidoId { get; set; }
        public int? PuestoId { get; set; }
        public int EleccionId { get; set; }
        public string FotoPerfil { get; set; }
        public bool? Estado { get; set; }

        public virtual Ciudadanos Ciudadano { get; set; }
        public virtual Elecciones Eleccion { get; set; }
        public virtual Partidos Partido { get; set; }
        public virtual PuestosElectivos Puesto { get; set; }
        public virtual ICollection<Votos> Votos { get; set; }
    }
}
