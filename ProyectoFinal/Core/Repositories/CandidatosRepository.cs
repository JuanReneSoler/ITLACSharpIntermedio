using Persistence;
using System.Linq;
using Core.Base;
using Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class CandidatosRepository : BaseRepository<Candidatos>
    {
        public CandidatosRepository(SVContext context) : base(context)
        {
        }

        public IQueryable<Candidatos> GetAllByEleccion(int EleccionId)
        {
            return base.Select(x => x.EleccionId == EleccionId)
                .Include(x => x.Ciudadano)
                .Include(x => x.Partido)
                .Include(x => x.Puesto);
        }

        public IQueryable<Candidatos> GetAllByEleccionAndPuesto(int EleccionId, int PuestoId)
        {
            return base.Select(x => x.EleccionId == EleccionId && x.PuestoId == PuestoId)
                .Include(x => x.Ciudadano)
                .Include(x => x.Partido)
                .Include(x => x.Puesto);
        }
    }
}