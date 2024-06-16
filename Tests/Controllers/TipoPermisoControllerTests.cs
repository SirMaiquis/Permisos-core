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
    public class TipoPermisoControllerTests
    {
        private TipoPermisoController _controller;
        private Mock<ITipoPermisoService> _mockTipoPermisoService;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mockTipoPermisoService = new Mock<ITipoPermisoService>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TipoPermisoProfile());
            });
            _mapper = mockMapper.CreateMapper();

            _controller = new TipoPermisoController(_mockTipoPermisoService.Object, _mapper);
        }

        [Test]
        public async Task GetAll_ReturnsAllItems()
        {
            // Arrange
            var tiposPermiso = new List<TipoPermiso>
            {
                new TipoPermiso { Id = 1, Descripcion = "Diligencias" },
                new TipoPermiso { Id = 2, Descripcion = "Enfermedad" }
            };
            _mockTipoPermisoService.Setup(service => service.GetAllAsync()).ReturnsAsync(tiposPermiso);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<IEnumerable<TipoPermisoDto>>>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var tiposPermisoDto = okResult.Value as IEnumerable<TipoPermisoDto>;
            Assert.That(tiposPermisoDto, Is.Not.Null);
            Assert.That(tiposPermisoDto.Count(), Is.EqualTo(2));
            var tipoPermisoDto = tiposPermisoDto.First();
            Assert.Multiple(() =>
            {
                Assert.That(tipoPermisoDto.Id, Is.EqualTo(tiposPermiso.First().Id));
                Assert.That(tipoPermisoDto.Descripcion, Is.EqualTo(tiposPermiso.First().Descripcion));
            });
        }

        [Test]
        public async Task GetById_ReturnsSingleItem()
        {
            // Arrange
            var tipoPermisoId = 1;
            var tipoPermiso = new TipoPermiso { Id = tipoPermisoId, Descripcion = "Otros" };
            _mockTipoPermisoService.Setup(service => service.GetByIdAsync(tipoPermisoId)).ReturnsAsync(tipoPermiso);

            // Act
            var result = await _controller.GetById(tipoPermisoId);

            // Assert
            Assert.That(result, Is.InstanceOf<ActionResult<TipoPermisoDto>>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            var tipoPermisoDto = okResult.Value as TipoPermisoDto;
            Assert.That(tipoPermisoDto, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(tipoPermisoDto.Id, Is.EqualTo(tipoPermiso.Id));
                Assert.That(tipoPermisoDto.Descripcion, Is.EqualTo(tipoPermiso.Descripcion));
            });
        }

        [Test]
        public async Task Create_ReturnsCreatedItem()
        {
            // Arrange
            var newTipoPermisoDto = new TipoPermisoDto { Descripcion = "Diligencias" };
            var tipoPermiso = _mapper.Map<TipoPermiso>(newTipoPermisoDto);
            _mockTipoPermisoService.Setup(service => service.CreateAsync(It.IsAny<TipoPermiso>())).ReturnsAsync(tipoPermiso);

            // Act
            var result = await _controller.Create(newTipoPermisoDto);

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
            var tipoPermisoId = 1;
            var updatedTipoPermisoDto = new TipoPermisoDto { Id = tipoPermisoId, Descripcion = "Otros" };
            var tipoPermiso = _mapper.Map<TipoPermiso>(updatedTipoPermisoDto);

            // Act
            var result = await _controller.Update(tipoPermisoId, updatedTipoPermisoDto);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockTipoPermisoService.Verify(service => service.UpdateAsync(It.IsAny<TipoPermiso>()), Times.Once);
        }

        [Test]
        public async Task Delete_ReturnsNoContent()
        {
            // Arrange
            var tipoPermisoId = 1;

            // Act
            var result = await _controller.Delete(tipoPermisoId);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _mockTipoPermisoService.Verify(service => service.DeleteAsync(tipoPermisoId), Times.Once);
        }
    }
}
