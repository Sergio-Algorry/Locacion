using Locacion.Comunes.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Locacion.Comunes.Data
{
    public class dbContext : DbContext
    {
        public DbSet<Pais> Paises { get; set; }

        public DbSet<Provincia> Provincias { get; set; }

        public dbContext(DbContextOptions<dbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .G­etEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership 
                        && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
