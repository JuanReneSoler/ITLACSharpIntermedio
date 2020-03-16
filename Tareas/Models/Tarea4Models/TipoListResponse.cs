
using Microsoft.AspNetCore.Http;

namespace Tareas.Models
{
    public class TipoListResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RGB { get; set; }
        public IFormFile Foto { get; set; }
        public string FotoPath { get; set; }
        public string FotoBase64 { get; set; }
    }
}