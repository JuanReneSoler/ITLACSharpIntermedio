namespace Models.Dtos
{
    public class CandidatosListResponse
    {
        public int? Id { get; set; }
        public string Foto { get; set; }
        public string NombreCompleto { get; set; }
        public string Puesto { get; set; }
        public string Partido { get; set; }
        public string PartidoFoto { get; set; }
        public string Estado { get; set; }
        public int Votos { get; set; }
    }
}