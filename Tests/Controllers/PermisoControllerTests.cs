using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Services.Interfaces;
using Services.Dtos;
using Data.Models;
using Services.Profiles;

namespace Tests.Controllers
{
    public class PermisoControllerTests
    {
        private PermisoController _controller;
        private Mock<IPermisoService> _mockPermisoService;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockPermisoService = new Mock<IPermisoService>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PermisoProfile());
            });
            _mapper = mockMapper.CreateMapper();

            _controller = new PermisoController(_mockPermisoService.Object, _mapper);
        }

        [Test]
        public async Task GetAll_ReturnsAllItems()
        {
            // Arrange
            var permisos = new List<Permiso>
            {
                new() { Id = 1, NombreEmpleado = "Juan", ApellidosEmpleado = "Perez", FechaPermiso = DateTime.Now, TipoPermisoId = 1 },
                new() { Id = 2, NombreEmpleado = "Ana", ApellidosEmpleado = "Gonzalez", FechaPermiso = DateTime.Now, TipoPermisoId = 2 }
            };
            _mockPermisoService.Setup(service => service.GetAllAsync()).ReturnsAsync(permisos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<PermisoDTO>>>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var permisosDto = okResult.Value as IEnumerable<PermisoDTO>;
            Assert.That(permisosDto, Is.Not.Null);
            Assert.That(permisosDto.Count(), Is.EqualTo(2));
            var permisoDto = permisosDto.First();
            Assert.Multiple(() =>
            {
                Assert.That(permisoDto.Id, Is.EqualTo(permisos.First().Id));
                Assert.That(permisoDto.NombreEmpleado, Is.EqualTo(permisos.First().NombreEmpleado));
                Assert.That(permisoDto.ApellidosEmpleado, Is.EqualTo(permisos.First().ApellidosEmpleado));
                Assert.That(permisoDto.FechaPermiso, Is.EqualTo(permisos.First().FechaPermiso));
                Assert.That(permisoDto.TipoPermisoId, Is.EqualTo(permisos.First().TipoPermisoId));
            });
        }

        [Test]
        public async Task GetById_ReturnsSingleItem()
        {
            // Arrange
            var permisoId = 1;
            var permiso = new Permiso { Id = permisoId, NombreEmpleado = "Juan", ApellidosEmpleado = "Perez", FechaPermiso = DateTime.Now, TipoPermisoId = 1 };
            _mockPermisoService.Setup(service => service.GetByIdAsync(permisoId)).ReturnsAsync(permiso);

            // Act
            var result = await _controller.GetById(permisoId);

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<PermisoDTO>>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var permisoDto = okResult.Value as PermisoDTO;
            Assert.That(permisoDto, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(permisoDto.Id, Is.EqualTo(permiso.Id));
                Assert.That(permisoDto.NombreEmpleado, Is.EqualTo(permiso.NombreEmpleado));
                Assert.That(permisoDto.ApellidosEmpleado, Is.EqualTo(permiso.ApellidosEmpleado));
                Assert.That(permisoDto.FechaPermiso, Is.EqualTo(permiso.FechaPermiso));
                Assert.That(permisoDto.TipoPermisoId, Is.EqualTo(permiso.TipoPermisoId));
            });
        }

        [Test]
        public async Task Create_ReturnsCreatedItem()
        {
            // Arrange
            var newPermisoDto = new PermisoDTO { NombreEmpleado = "Carlos", ApellidosEmpleado = "Martinez", FechaPermiso = DateTime.Now, TipoPermisoId = 2 };
            var permiso = _mapper.Map<Permiso>(newPermisoDto);
            _mockPermisoService.Setup(service => service.CreateAsync(It.IsAny<Permiso>())).ReturnsAsync(permiso);

            // Act
            var result = await _controller.Create(newPermisoDto);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = result as CreatedAtActionResult;
            Assert.That(createdResult, Is.Not.Null);
            Assert.That(createdResult.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public async Task Update_ReturnsNoContent()
        {
            // Arrange
            var permisoId = 1;
            var updatedPermisoDto = new PermisoDTO { Id = permisoId, NombreEmpleado = "Pedro", ApellidosEmpleado = "Lopez", FechaPermiso = DateTime.Now, TipoPermisoId = 3 };
            var permiso = _mapper.Map<Permiso>(updatedPermisoDto);

            // Act
            var result = await _controller.Update(permisoId, updatedPermisoDto);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockPermisoService.Verify(service => service.UpdateAsync(It.IsAny<Permiso>()), Times.Once);
        }

        [Test]
        public async Task Delete_ReturnsNoContent()
        {
            // Arrange
            var permisoId = 1;

            // Act
            var result = await _controller.Delete(permisoId);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockPermisoService.Verify(service => service.DeleteAsync(permisoId), Times.Once);
        }
    }
}
