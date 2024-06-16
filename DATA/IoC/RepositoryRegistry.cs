using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.IoC
{
     public static class RepositoryRegistry
     {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPermisoRepository, PermisoRepository>();
            services.AddTransient<ITipoPermisoRepository, TipoPermisoRepository>();
        }
     }
}
