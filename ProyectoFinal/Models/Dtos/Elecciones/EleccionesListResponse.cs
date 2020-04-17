using System.Collections.Generic;

namespace Models.Dtos
{
    public class EleccionesListResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public int Estado { get; set; }
        public string EstadoDescripcion { get; set; }
        public int Votos { get; set; }
    }
}