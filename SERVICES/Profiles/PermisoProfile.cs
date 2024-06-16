using Data.Models;
using Services.Dtos;
using AutoMapper;
using System.Security;

namespace Services.Profiles
{
    public class PermisoProfile : Profile
    {
        public PermisoProfile()
        {
            CreateMap<Permiso, PermisoDTO>()
               .ForMember(y => y.TipoPermisoDescripcion, x => x.MapFrom(y => y.TipoPermiso.Descripcion));
            CreateMap<PermisoDTO, Permiso>();
        }
    }
}
