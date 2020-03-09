
using Tareas.Models;
namespace Tareas.CtxTarea4
{
    public partial class Pokemon
    {
        public static explicit operator PokemonListResponse(Pokemon v)
        {
            return new PokemonListResponse
            {
                nombre = v.Nombre,
                region = v.Region.Nombre,
                tipo = string.Join(',',v.TipoPokemon)
            };
        }
    }

    public partial class Region
    {
        public static explicit operator Region(RegionesListResponse v)
        {
            return new Region
            {
                Id = v.Id,
                Nombre = v.Nombre,
                Rgbcolor = v.RGB
            };
        }
        public static explicit operator RegionesListResponse(Region v)
        {
            return new RegionesListResponse
            {
                Id = v.Id,
                Nombre = v.Nombre,
                RGB = v.Rgbcolor
            };
        }
    }

    public partial class Tipo
    {
        public static explicit operator Tipo(TipoListResponse v)
        {
            return new Tipo
            {
                Id = v.Id,
                Nombre = v.Nombre,
            };
        }
        public static explicit operator TipoListResponse(Tipo v)
        {
            return new TipoListResponse
            {
                Id = v.Id,
                Nombre = v.Nombre,
            };
        }
    }
}



