using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Elecciones
    {
        public Elecciones()
        {
            Candidatos = new HashSet<Candidatos>();
            Votantes = new HashSet<Votantes>();
            Votos = new HashSet<Votos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRealizadion { get; set; }
        public int EstadosId { get; set; }

        public virtual EstadosElecciones Estados { get; set; }
        public virtual ICollection<Candidatos> Candidatos { get; set; }
        public virtual ICollection<Votantes> Votantes { get; set; }
        public virtual ICollection<Votos> Votos { get; set; }
    }
}
