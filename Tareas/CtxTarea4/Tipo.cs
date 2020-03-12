using System;
using System.Collections.Generic;

namespace Tareas.CtxTarea4
{
    public partial class Tipo
    {
        public Tipo()
        {
            TipoPokemon = new HashSet<TipoPokemon>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rgbcolor { get; set; }
        public string FotoPath { get; set; }

        public virtual ICollection<TipoPokemon> TipoPokemon { get; set; }
    }
}
