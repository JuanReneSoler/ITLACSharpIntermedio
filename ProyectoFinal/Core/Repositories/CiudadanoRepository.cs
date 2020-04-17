using Persistence;
using Core.Base;
using Models.Entities;

namespace Core.Repositories
{
    public class CiudadanoRepository: BaseRepository<Ciudadanos>
    {
        public CiudadanoRepository(SVContext context):base(context)
        {
        }
    }
}