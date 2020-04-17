using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base;
using Models.Entities;
using Models.Dtos;

namespace Core.Repositories
{
    public class VotacionRepository : BaseRepository<Votantes>//, IVotantesRepository
    {
        public VotacionRepository(SVContext context) : base(context)
        {
        }

        public async Task Add(VotacionRequest ciudadano)
        {
            var entity = (Votantes)ciudadano;
            var list = new List<Votos>();
            foreach (var item in ciudadano.CandidatosId)
            {
                if(item != 0)
                {
                    var voto = new Votos
                    {
                        CandidatoId = item,
                        EleccionId = ciudadano.EleccionId,
                        Id = 0,
                    };
                    list.Add(voto);
                }
                
            }
            base.dbContext.Set<Votos>().AddRange(list);
            await base.Entity.AddAsync(entity);
        }
    }
}
