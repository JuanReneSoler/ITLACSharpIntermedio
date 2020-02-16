using System;
using System.Collections.Generic;

namespace Tareas.CtxTarea3
{
    public partial class Estudiantes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int CarreraId { get; set; }
        public bool Estado { get; set; }
    }
}
