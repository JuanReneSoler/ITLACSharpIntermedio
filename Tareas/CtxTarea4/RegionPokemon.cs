using System;
using System.Collections.Generic;

namespace Tareas.CtxTarea4
{
    public partial class RegionPokemon
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int RegionId { get; set; }

        public virtual Pokemon Pokemon { get; set; }
        public virtual Region Region { get; set; }
    }
}
