using Microsoft.EntityFrameworkCore;
using Seguros.Modelo;

namespace Seguros.Migrations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Modelo.Seguro> Seguros { get; set; }
        public DbSet<Modelo.Asegurado> Asegurados { get; set; }
    }
}
