using Moq;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;
using Data.Repositories;

namespace Tests.Repositories
{
    public class PermisoRepositoryTests
    {
        private PermisoRepository _permisoRepository;
        private Mock<ApplicationDbContext> _mockContext;
        private Mock<DbSet<Permiso>> _mockSet;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockSet = new Mock<DbSet<Permiso>>();

            _mockContext.Setup(c => c.Set<Permiso>()).Returns(_mockSet.Object);

            _permisoRepository = new PermisoRepository(_mockContext.Object);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsSingleItem()
        {
            // Arrange
            var permisoId = 1;
            var permiso = new Permiso { Id = permisoId, NombreEmpleado = "Juan", ApellidosEmpleado = "Perez" };
            _mockSet.Setup(m => m.FindAsync(permisoId)).ReturnsAsync(permiso);

            // Act
            var result = await _permisoRepository.GetByIdAsync(permisoId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.NombreEmpleado, Is.EqualTo(permiso.NombreEmpleado));
        }

        [Test]
        public async Task AddAsync_AddsNewItem()
        {
            // Arrange
            var newPermiso = new Permiso { NombreEmpleado = "Maria", ApellidosEmpleado = "Lopez" };

            // Act
            await _permisoRepository.AddAsync(newPermiso);

            // Assert
            _mockSet.Verify(m => m.AddAsync(newPermiso, default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_UpdatesItem()
        {
            // Arrange
            var updatedPermiso = new Permiso { Id = 1, NombreEmpleado = "Juan Updated", ApellidosEmpleado = "Perez" };

            // Act
            await _permisoRepository.UpdateAsync(updatedPermiso);

            // Assert
            _mockSet.Verify(m => m.Update(updatedPermiso), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_DeletesItem()
        {
            // Arrange
            var permisoId = 1;
            var permiso = new Permiso { Id = permisoId };

            // Act
            _mockSet.Setup(m => m.FindAsync(permisoId)).ReturnsAsync(permiso);
            await _permisoRepository.DeleteAsync(permisoId);

            // Assert
            _mockSet.Verify(m => m.FindAsync(permisoId), Times.Once);
            _mockSet.Verify(m => m.Remove(permiso), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
