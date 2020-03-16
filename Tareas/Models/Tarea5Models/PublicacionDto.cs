using System.ComponentModel.DataAnnotations;

namespace Tareas.Models
{
    public class PublicacionDto
    {
        public int Id { get; set; }
        [Display(Name="Titulo")]
        [Required(ErrorMessage="Titulo es requerido")]
        public string Title { get; set; }

        [Display(Name="Contenido de la publicacion")]
        [Required(ErrorMessage="Contenido requerido")]
        public string Contenido { get; set; }
    }
}