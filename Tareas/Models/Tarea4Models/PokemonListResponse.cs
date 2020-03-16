
namespace Tareas.Models
{
    public class PokemonListResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoListResponse[] Tipos { get; set; }
        public RegionesListResponse[] Regiones { get; set; }
        public string FotoBase64 { get; set; }
    }
}

