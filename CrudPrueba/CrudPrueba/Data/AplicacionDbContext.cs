using CrudPrueba.Shared;
using Microsoft.EntityFrameworkCore;


namespace CrudPrueba.Data
{
    public class AplicacionDbContext : DbContext
    {
        public AplicacionDbContext(DbContextOptions<AplicacionDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
