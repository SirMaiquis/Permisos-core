using Data.Interfaces;
using Data.Models;
using Services.Interfaces;

namespace Services.Services
{
    public class PermisoService(IPermisoRepository permisoRepository) : IPermisoService
    {
        private readonly IPermisoRepository _permisoRepository = permisoRepository;

        public async Task<IEnumerable<Permiso>> GetAllAsync()
        {
            return await _permisoRepository.GetAllAsync();
        }

        public async Task<Permiso> GetByIdAsync(int id)
        {
            return await _permisoRepository.GetByIdAsync(id);
        }

        public async Task<Permiso> CreateAsync(Permiso permiso)
        {
            await _permisoRepository.AddAsync(permiso);
            return permiso;
        }

        public async Task UpdateAsync(Permiso permiso)
        {
            await _permisoRepository.UpdateAsync(permiso);
        }

        public async Task DeleteAsync(int id)
        {
            await _permisoRepository.DeleteAsync(id);
        }
    }
}
