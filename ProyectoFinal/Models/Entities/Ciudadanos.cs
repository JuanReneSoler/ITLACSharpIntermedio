using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Ciudadanos
    {
        public Ciudadanos()
        {
            Candidatos = new HashSet<Candidatos>();
            Votantes = new HashSet<Votantes>();
        }

        public int Id { get; set; }
        public string DocIdentidad { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Candidatos> Candidatos { get; set; }
        public virtual ICollection<Votantes> Votantes { get; set; }
    }
}
