using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Data.Models;
using Services.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class TipoPermisoController(ITipoPermisoService tipoPermisoService, IMapper mapper) : BaseController<TipoPermiso, ITipoPermisoService, TipoPermisoDto>(tipoPermisoService, mapper)
    {
    }
}
