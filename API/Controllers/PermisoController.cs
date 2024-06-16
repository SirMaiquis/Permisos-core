using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Data.Models;
using Services.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class PermisoController(IPermisoService permisoService, IMapper mapper) : BaseController<Permiso, IPermisoService, PermisoDTO>(permisoService, mapper)
    {
    }
}
