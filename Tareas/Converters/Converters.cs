
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
                Nombre = v.Nonmbre,
                Rgbcolor = v.RGB
            };
        }
        public static explicit operator RegionesListResponse(Region v)
        {
            return new RegionesListResponse
            {
                Id = v.Id,
                Nonmbre = v.Nombre,
                RGB = v.Rgbcolor
            };
        }
    }
}



