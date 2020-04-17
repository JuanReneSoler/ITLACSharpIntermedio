using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVotaciones.Models
{
    public class PaginationDto
    {
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int Length { get; set; }
        public bool HasNext { get { return CurrentPage < Pages; } }
        public bool HasPrevius { get { return this.Pages > 1 && this.CurrentPage > 1 && this.CurrentPage<=this.Pages; } }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
