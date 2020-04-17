using Persistence;
using Core.Base;
using Models.Entities;
using System.Linq;

namespace Core.Repositories
{
    public class PuestosRepository : BaseRepository<PuestosElectivos>
    {
        public PuestosRepository(SVContext context) : base(context)
        {
        }

        public IQueryable<PuestosElectivos> GetByEleccionId(int EleccionId)
        {
            return (from a in this.Entity
                   join b in this.dbContext.Candidatos on a.Id equals b.PuestoId
                   where b.EleccionId == EleccionId
                   select a).Distinct();
        }
    }
}