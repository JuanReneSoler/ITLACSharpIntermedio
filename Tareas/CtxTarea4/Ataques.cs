using System;
using System.Collections.Generic;

namespace Tareas.CtxTarea4
{
    public partial class Ataques
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public string Nombre { get; set; }

        public virtual Pokemon Pokemon { get; set; }
    }
}
