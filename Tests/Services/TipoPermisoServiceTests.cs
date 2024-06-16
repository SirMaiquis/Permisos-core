using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Data.Interfaces;
using Services.Services;

namespace Tests.Services
{
    public class TipoPermisoServiceTests
    {
        private TipoPermisoService _tipoPermisoService;
        private Mock<ITipoPermisoRepository> _mockTipoPermisoRepository;

        [SetUp]
        public void Setup()
        {
            _mockTipoPermisoRepository = new Mock<ITipoPermisoRepository>();
            _tipoPermisoService = new TipoPermisoService(_mockTipoPermisoRepository.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllItems()
        {
            // Arrange
            var tiposPermiso = new List<TipoPermiso>
            {
                new TipoPermiso { Id = 1, Descripcion = "Diligencias" },
                new TipoPermiso { Id = 2, Descripcion = "Enfermedad" }
            };
            _mockTipoPermisoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tiposPermiso);

            // Act
            var result = await _tipoPermisoService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Descripcion, Is.EqualTo(tiposPermiso.First().Descripcion));
        }

        [Test]
        public async Task GetByIdAsync_ReturnsSingleItem()
        {
            // Arrange
            var tipoPermisoId = 1;
            var tipoPermiso = new TipoPermiso { Id = tipoPermisoId, Descripcion = "Diligencias" };
            _mockTipoPermisoRepository.Setup(repo => repo.GetByIdAsync(tipoPermisoId)).ReturnsAsync(tipoPermiso);

            // Act
            var result = await _tipoPermisoService.GetByIdAsync(tipoPermisoId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Descripcion, Is.EqualTo(tipoPermiso.Descripcion));
        }

        [Test]
        public async Task CreateAsync_ReturnsCreatedItem()
        {
            // Arrange
            var newTipoPermiso = new TipoPermiso { Descripcion = "Otros" };

            // Act
            var result = await _tipoPermisoService.CreateAsync(newTipoPermiso);

            // Assert
            Assert.That(result, Is.Not.Null);
            _mockTipoPermisoRepository.Verify(repo => repo.AddAsync(newTipoPermiso), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_UpdatesItem()
        {
            // Arrange
            var updatedTipoPermiso = new TipoPermiso { Id = 1, Descripcion = "Diligencias" };

            // Act
            await _tipoPermisoService.UpdateAsync(updatedTipoPermiso);

            // Assert
            _mockTipoPermisoRepository.Verify(repo => repo.UpdateAsync(updatedTipoPermiso), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_DeletesItem()
        {
            // Arrange
            var tipoPermisoId = 1;

            // Act
            await _tipoPermisoService.DeleteAsync(tipoPermisoId);

            // Assert
            _mockTipoPermisoRepository.Verify(repo => repo.DeleteAsync(tipoPermisoId), Times.Once);
        }
    }
}
