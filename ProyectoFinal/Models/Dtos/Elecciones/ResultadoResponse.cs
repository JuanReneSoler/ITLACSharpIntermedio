using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class ResultadoResponse
    {
        public string PuestoName { get; set; }
        public List<CandidatosListResponse> Candidatos { get; set; }
    }
}
