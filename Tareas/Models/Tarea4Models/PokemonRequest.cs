
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;
namespace Tareas.Models
{
    public class PokemonRequest
    {
        public int Id { get; set; }

        [DisplayName("Nombre del Pokemon")]
        public string Nombre { get; set; }

        [DisplayName("Seleccion Region del Pokemon")]
        public int[] RegionesId { get; set; }

        public ICollection<dynamic> RegionesObj { get; set; }

        [DisplayName("Seleccione Tipo de Pokemon")]
        public int[] TiposId { get; set; }

        public ICollection<dynamic> TiposObj { get; set; }

        [DisplayName("Seleccione Ataquies del Pokemon")]
        public string[] Ataques { get; set; }

        public ICollection<dynamic> AtaquesObj { get; set; }

        [DisplayName("Imagen del Pokemon")]
        public IFormFile Foto { get; set; }
        public string FotoPath { get; set; }
        public string FotoBase64 { get; set; }
    }
}