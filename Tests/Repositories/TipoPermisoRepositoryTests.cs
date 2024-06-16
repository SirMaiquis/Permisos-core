using Moq;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;
using Data.Repositories;

namespace Tests.Repositories
{
    public class TipoPermisoRepositoryTests
    {
        private TipoPermisoRepository _tipoPermisoRepository;
        private Mock<ApplicationDbContext> _mockContext;
        private Mock<DbSet<TipoPermiso>> _mockSet;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockSet = new Mock<DbSet<TipoPermiso>>();

            _mockContext.Setup(c => c.Set<TipoPermiso>()).Returns(_mockSet.Object);

            _tipoPermisoRepository = new TipoPermisoRepository(_mockContext.Object);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsSingleItem()
        {
            // Arrange
            var tipoPermisoId = 1;
            var tipoPermiso = new TipoPermiso { Id = tipoPermisoId, Descripcion = "Diligencias" };
            _mockSet.Setup(m => m.FindAsync(tipoPermisoId)).ReturnsAsync(tipoPermiso);

            // Act
            var result = await _tipoPermisoRepository.GetByIdAsync(tipoPermisoId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Descripcion, Is.EqualTo(tipoPermiso.Descripcion));
        }

        [Test]
        public async Task AddAsync_AddsNewItem()
        {
            // Arrange
            var newTipoPermiso = new TipoPermiso { Descripcion = "Diligencias" };

            // Act
            await _tipoPermisoRepository.AddAsync(newTipoPermiso);

            // Assert
            _mockSet.Verify(m => m.AddAsync(newTipoPermiso, default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_UpdatesItem()
        {
            // Arrange
            var updatedTipoPermiso = new TipoPermiso { Id = 1, Descripcion = "Diligencias" };

            // Act
            await _tipoPermisoRepository.UpdateAsync(updatedTipoPermiso);

            // Assert
            _mockSet.Verify(m => m.Update(updatedTipoPermiso), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_DeletesItem()
        {
            // Arrange
            var tipoPermisoId = 1;
            var tipoPermiso = new TipoPermiso { Id = tipoPermisoId };

            // Act
            _mockSet.Setup(m => m.FindAsync(tipoPermisoId)).ReturnsAsync(tipoPermiso);
            await _tipoPermisoRepository.DeleteAsync(tipoPermisoId);

            // Assert
            _mockSet.Verify(m => m.FindAsync(tipoPermisoId), Times.Once);
            _mockSet.Verify(m => m.Remove(tipoPermiso), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
