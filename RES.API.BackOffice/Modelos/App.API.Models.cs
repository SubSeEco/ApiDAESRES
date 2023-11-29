using DAES.API.BackOffice.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DAES.API.BackOffice.Modelos
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<Organizacion> Organizacion { get; set; }

        // Otro código de configuración de DbContext
    }
}


