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
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<SubRubro> SubRubros { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Comuna> Comunas { get; set; }
        public DbSet<DiccionarioRubro> DiccionarioRubros { get; set; }
        public DbSet<DiccionarioSubRubro> DiccionarioSubRubros { get; set; }
        public DbSet<DiccionarioRegion> DiccionarioRegiones { get; set; }
        public DbSet<DiccionarioComuna> DiccionarioComunas { get; set; }
        public DbSet<RESCrearOrgMensaje> RESCrearOrgMensajes { get; set; }
        public DbSet<RESCrearOrgobjetoSocial_actividades> RESCrearOrgObjetoSocial_actividades { get; set; }
        public DbSet<RESCrearOrgCooperadosYAdministradores> RESCrearOrgCooperadosYAdministradores { get; set; }
        public DbSet<RESCrearOrgCapitalDelSocio> RESCrearOrgCapitalDelSocios { get; set; }
        public DbSet<RESCrearOrgRepresentante> RESCrearOrgRepresentantes { get; set; }
        public DbSet<RESCrearOrgCalidad> RESCrearOrgCalidades { get; set; }

        // Otro código de configuración de DbContext
    }
}


