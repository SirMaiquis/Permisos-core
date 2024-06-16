using Microsoft.Extensions.DependencyInjection;
using Services.Profiles;

namespace Services.IoC
{
    public static class ProfileRegistry
    {
        public static void AddProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PermisoProfile));
            services.AddAutoMapper(typeof(TipoPermisoProfile));
        }
    }
}
