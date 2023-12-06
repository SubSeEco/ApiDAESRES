using DAES.API.BackOffice.Modelos;
using Microsoft.EntityFrameworkCore;
using RES.API.BackOffice.Modelos;

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

        public DbSet<Organizacion> Organizaciones { get; set; }
        public DbSet<ActualizacionOrganizacion> ActualizacionOrganizaciones { get; set; }
        public DbSet<Proceso> Procesos { get; set; }
        public DbSet<Solicitante> Solicitantes { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<DefinicionProceso> DefinicionProcesos { get; set; }
        public DbSet<DefinicionWorkflow> DefinicionWorkflows { get; set; }
        public DbSet<AspNetUsers> NetUsers { get; set; }
        public DbSet<Rol> Roles { get; set; }

        // Otro código de configuración de DbContext
    }
}


