using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Services;

namespace Services.IoC
{
    public static class ServiceRegistry
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPermisoService, PermisoService>();
            services.AddTransient<ITipoPermisoService, TipoPermisoService>();
        }
    }
}
