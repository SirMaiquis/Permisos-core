using Data.Context;
using Data.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PermisoRepository(ApplicationDbContext context) : BaseRepository<Permiso>(context), IPermisoRepository
    {
        public async Task<IEnumerable<Permiso>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.TipoPermiso)
                .ToListAsync();
        }
    }
}
