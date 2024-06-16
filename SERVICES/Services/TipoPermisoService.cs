using Data.Interfaces;
using Data.Models;
using Services.Interfaces;

namespace Services.Services
{
    public class TipoPermisoService(ITipoPermisoRepository tipoPermisoRepository) : ITipoPermisoService
    {
        private readonly ITipoPermisoRepository _tipoPermisoRepository = tipoPermisoRepository;

        public async Task<IEnumerable<TipoPermiso>> GetAllAsync()
        {
            return await _tipoPermisoRepository.GetAllAsync();
        }

        public async Task<TipoPermiso> GetByIdAsync(int id)
        {
            return await _tipoPermisoRepository.GetByIdAsync(id);
        }

        public async Task<TipoPermiso> CreateAsync(TipoPermiso tipoPermiso)
        {
            await _tipoPermisoRepository.AddAsync(tipoPermiso);
            return tipoPermiso;
        }

        public async Task UpdateAsync(TipoPermiso tipoPermiso)
        {
            await _tipoPermisoRepository.UpdateAsync(tipoPermiso);
        }

        public async Task DeleteAsync(int id)
        {
            await _tipoPermisoRepository.DeleteAsync(id);
        }
    }
}
