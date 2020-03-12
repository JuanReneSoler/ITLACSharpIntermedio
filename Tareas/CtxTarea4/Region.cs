using System;
using System.Collections.Generic;

namespace Tareas.CtxTarea4
{
    public partial class Region
    {
        public Region()
        {
            RegionPokemon = new HashSet<RegionPokemon>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rgbcolor { get; set; }

        public virtual ICollection<RegionPokemon> RegionPokemon { get; set; }
    }
}
