using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.Property(p => p.TipoPermisoId)
                .IsRequired();

            builder.HasOne(p => p.TipoPermiso)
                .WithMany()
                .HasForeignKey(p => p.TipoPermisoId);
        }
    }
}
