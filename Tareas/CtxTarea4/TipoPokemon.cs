using System;
using System.Collections.Generic;

namespace Tareas.CtxTarea4
{
    public partial class TipoPokemon
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int TipoId { get; set; }

        public virtual Pokemon Pokemon { get; set; }
        public virtual Tipo Tipo { get; set; }
    }
}
