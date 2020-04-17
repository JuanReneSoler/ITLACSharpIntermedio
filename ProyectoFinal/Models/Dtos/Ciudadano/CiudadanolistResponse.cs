namespace Models.Dtos
{
    public partial class CiudadanoListResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string NombreCompleto
        { 
            get
            {
                return $"{this.Nombre} {this.Apellido}";
            }
        }
        
        public string Estado { get; set; }
    }
}