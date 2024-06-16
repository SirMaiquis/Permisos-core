using Data.Context;
using Data.Models;
using Data.Interfaces;

namespace Data.Repositories
{
    public class TipoPermisoRepository(ApplicationDbContext context) : BaseRepository<TipoPermiso>(context), ITipoPermisoRepository
    {
    }
}
