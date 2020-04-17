using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class PuestosElectivos
    {
        public PuestosElectivos()
        {
            Candidatos = new HashSet<Candidatos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Candidatos> Candidatos { get; set; }
    }
}
