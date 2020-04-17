using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class EstadosElecciones
    {
        public EstadosElecciones()
        {
            Elecciones = new HashSet<Elecciones>();
        }

        public int Id { get; set; }
        public string DescripcionEstado { get; set; }

        public virtual ICollection<Elecciones> Elecciones { get; set; }
    }
}
