using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Configuration;
using Data.Configurations;

namespace Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public ApplicationDbContext()
        {
            
        }

        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<TipoPermiso> TipoPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PermisoConfiguration());
            modelBuilder.ApplyConfiguration(new TipoPermisoConfiguration());

            base.OnModelCreating(modelBuilder);     
        }

        public override int SaveChanges()
        {
            AddAuditChanges();
            return base.SaveChanges();
        }

        private void AddAuditChanges()
        {
            var entries = ChangeTracker.Entries()
                            .Where(e => e.Entity is BaseEntity && (
                                    e.State == EntityState.Added ||
                                    e.State == EntityState.Modified ||
                                    e.State == EntityState.Deleted));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;
                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }

                if (entityEntry.State == EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Modified;
                    entity.Deleted = DateTime.UtcNow;
                }
            }
        }
    }
}
