using System;
using System.Collections.Generic;

namespace Tareas.CtxTarea5
{
    public partial class Publicacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string IdUser { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}
