﻿using System;
using System.Collections.Generic;

namespace Tareas.CtxTarea4
{
    public partial class Pokemon
    {
        public Pokemon()
        {
            Ataques = new HashSet<Ataques>();
            RegionPokemon = new HashSet<RegionPokemon>();
            TipoPokemon = new HashSet<TipoPokemon>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string FotoPath { get; set; }

        public virtual ICollection<Ataques> Ataques { get; set; }
        public virtual ICollection<RegionPokemon> RegionPokemon { get; set; }
        public virtual ICollection<TipoPokemon> TipoPokemon { get; set; }
    }
}
