using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntity, TService, TEntityDto>(TService service, IMapper mapper) : ControllerBase
        where TEntity : class
        where TService : IBaseService<TEntity>
        where TEntityDto : class
    {
        protected readonly TService _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntityDto>>> GetAll()
        {
            var entities = await ((dynamic)_service).GetAllAsync();
            var entitiesDto = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDto>>(entities);
            return Ok(entitiesDto);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntityDto>> GetById(int id)
        {
            var entity = await ((dynamic)_service).GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var entityDto = _mapper.Map<TEntity, TEntityDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TEntityDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity =_mapper.Map<TEntityDto, TEntity>(entityDto);
            var createdEntity = await ((dynamic)_service).CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id, TEntityDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<TEntityDto, TEntity>(entityDto);

            await ((dynamic)_service).UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await ((dynamic)_service).DeleteAsync(id);
            return NoContent();
        }
    }
}
