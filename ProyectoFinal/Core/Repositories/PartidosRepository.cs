using Persistence;
using Core.Base;
using Models.Entities;

namespace Core.Repositories
{
    public class PartidosRepository:BaseRepository<Partidos>//, IPartidosRepository
    {
        public PartidosRepository(SVContext context):base(context)
        {
        }
    }
}