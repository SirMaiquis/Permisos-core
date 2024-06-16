using Data.Models;
using Services.Dtos;
using AutoMapper;

namespace Services.Profiles
{
    public class TipoPermisoProfile : Profile
    {
        public TipoPermisoProfile() {
            CreateMap<TipoPermiso, TipoPermisoDto>();
            CreateMap<TipoPermisoDto, TipoPermiso>();
        }
    }
}
