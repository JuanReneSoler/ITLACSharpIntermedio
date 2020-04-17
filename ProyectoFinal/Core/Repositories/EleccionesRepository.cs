using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Entities;
using Models.Dtos;
using Core.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class EleccionesRepository : BaseRepository<Elecciones>//,IEleccionesRepository
    {
        public EleccionesRepository(SVContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ResultadoResponse>> ResultadoEleccion(int id)
        {
            var query = (from a in this.dbContext.PuestosElectivos
                                join b in this.dbContext.Candidatos on a.Id equals b.PuestoId
                                where b.EleccionId == id
                                select a).Distinct();

            query = query
                .Include(x => x.Candidatos).ThenInclude(x=>x.Votos)
                .Include(x => x.Candidatos).ThenInclude(x=>x.Ciudadano)
                .Include(x => x.Candidatos).ThenInclude(x=>x.Puesto)
                .Include(x => x.Candidatos).ThenInclude(x=>x.Partido);

            var result = await query.ToListAsync();

            result.ForEach(x=>x.Candidatos = x.Candidatos.Where(y=>y.EleccionId == id).ToList());

            return result.Select(x => (ResultadoResponse)x).Select(x =>
            {
                x.Candidatos.OrderBy(y => y.Votos);
                return x;
            });

            

        }
    }
}