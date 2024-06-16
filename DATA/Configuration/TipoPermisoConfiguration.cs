using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Models;

namespace Data.Configuration
{
    public class TipoPermisoConfiguration : IEntityTypeConfiguration<TipoPermiso>
    {
        public void Configure(EntityTypeBuilder<TipoPermiso> builder)
        {
            builder.HasData(
                new TipoPermiso { Id = 1, Descripcion = "Enfermedad", CreatedAt = DateTime.UtcNow },
                new TipoPermiso { Id = 2, Descripcion = "Diligencias", CreatedAt = DateTime.UtcNow },
                new TipoPermiso { Id = 3, Descripcion = "Otros", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
