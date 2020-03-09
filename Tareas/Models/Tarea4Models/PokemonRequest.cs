
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Tareas.Models
{
    public class PokemonRequest
    {
        public int Id { get; set; }

        [DisplayName("Nombre del Pokemon")]
        public string Nombre { get; set; }

        [DisplayName("Seleccion Region del Pokemon")]
        public int[] RegionesId { get; set; }

        [DisplayName("Seleccione Tipo de Pokemon")]
        public int[] TiposId { get; set; }

        [DisplayName("Seleccione Ataquies del Pokemon")]
        public string[] AtaquesId { get; set; }
    }
}