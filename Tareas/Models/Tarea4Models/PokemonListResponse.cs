
namespace Tareas.Models
{
    public class PokemonListResponse
    {
        public string nombre { get; set; }
        public TipoListResponse[] tipos { get; set; }
        public RegionesListResponse regiones { get; set; }
        public string fotoBase64 { get; set; }
    }
}

