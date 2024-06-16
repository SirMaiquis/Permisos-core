using Moq;
using Data.Models;
using Data.Interfaces;
using Services.Services;

namespace Tests.Services
{
    public class PermisoServiceTests
    {
        private PermisoService _permisoService;
        private Mock<IPermisoRepository> _mockPermisoRepository;

        [SetUp]
        public void Setup()
        {
            _mockPermisoRepository = new Mock<IPermisoRepository>();
            _permisoService = new PermisoService(_mockPermisoRepository.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllItems()
        {
            // Arrange
            var permisos = new List<Permiso>
            {
                new Permiso { Id = 1, NombreEmpleado = "Juan", ApellidosEmpleado = "Perez" },
                new Permiso { Id = 2, NombreEmpleado = "Ana", ApellidosEmpleado = "Gonzalez" }
            };
            _mockPermisoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(permisos);

            // Act
            var result = await _permisoService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().NombreEmpleado, Is.EqualTo(permisos.First().NombreEmpleado));
        }

        [Test]
        public async Task GetByIdAsync_ReturnsSingleItem()
        {
            // Arrange
            var permisoId = 1;
            var permiso = new Permiso { Id = permisoId, NombreEmpleado = "Juan", ApellidosEmpleado = "Perez" };
            _mockPermisoRepository.Setup(repo => repo.GetByIdAsync(permisoId)).ReturnsAsync(permiso);

            // Act
            var result = await _permisoService.GetByIdAsync(permisoId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.NombreEmpleado, Is.EqualTo(permiso.NombreEmpleado));
        }

        [Test]
        public async Task CreateAsync_ReturnsCreatedItem()
        {
            // Arrange
            var newPermiso = new Permiso { NombreEmpleado = "Maria", ApellidosEmpleado = "Lopez" };

            // Act
            var result = await _permisoService.CreateAsync(newPermiso);

            // Assert
            Assert.That(result, Is.Not.Null);
            _mockPermisoRepository.Verify(repo => repo.AddAsync(newPermiso), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_UpdatesItem()
        {
            // Arrange
            var updatedPermiso = new Permiso { Id = 1, NombreEmpleado = "Juan Updated", ApellidosEmpleado = "Perez" };

            // Act
            await _permisoService.UpdateAsync(updatedPermiso);

            // Assert
            _mockPermisoRepository.Verify(repo => repo.UpdateAsync(updatedPermiso), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_DeletesItem()
        {
            // Arrange
            var permisoId = 1;

            // Act
            await _permisoService.DeleteAsync(permisoId);

            // Assert
            _mockPermisoRepository.Verify(repo => repo.DeleteAsync(permisoId), Times.Once);
        }
    }
}
