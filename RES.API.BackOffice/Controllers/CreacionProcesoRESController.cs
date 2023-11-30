using System.Text.Json;
using NJsonSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NJsonSchema.Validation;
using RES.API.BackOffice;
using DAES.API.BackOffice.Modelos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreacionProcesoRESController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly MyDbContext _dbContext;

        public CreacionProcesoRESController(IMemoryCache cache, MyDbContext dbContext)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        // POST api/CreacionProcesoRES
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] JsonDocument jsonDocument)
        {
            MensajeOrganizacionRES mensajeOrganizacionesRES = new MensajeOrganizacionRES();
            try
            {
                _cache.TryGetValue("MensajeOrganizacionRES", out string organizacionRESSchema);
                var schema = JsonSchema.FromJsonAsync(organizacionRESSchema).Result;
                var validator = new JsonSchemaValidator();
                var validation = validator.Validate(jsonDocument.RootElement.ToString(), schema);
                if (validation.Count != 0) { return BadRequest("{\"message\": \"json invalido\"}"); }

                mensajeOrganizacionesRES = jsonDocument.Deserialize<MensajeOrganizacionRES>();

            }
            catch (Exception e)
            {
                return BadRequest("{\"message\": \"no se pudo deserializar el json\"}");
            }


            Solicitante solicitante = new Solicitante()
            {
                Rut = mensajeOrganizacionesRES.DatosDelSistema.RutDV,
                Nombres = mensajeOrganizacionesRES.DatosDelSistema.NombresSolicitante,
                Apellidos = mensajeOrganizacionesRES.DatosDelSistema.PrimerApellido + " " + mensajeOrganizacionesRES.DatosDelSistema.SegundoApellido,
                Email = mensajeOrganizacionesRES.DatosDelSistema.EmailSolicitante,
                Fono = mensajeOrganizacionesRES.DatosDelSistema.FonoSolicitante
                //RegionId =
            };

            var solicitanteExistente = _dbContext.Solicitantes.Where(s => s.Rut == solicitante.Rut &&
                                                                          s.Nombres == solicitante.Nombres &&
                                                                          s.Apellidos == solicitante.Apellidos &&
                                                                          s.Email == solicitante.Email &&
                                                                          s.Fono == solicitante.Fono);
            if (!solicitanteExistente.Any())
            {
                _dbContext.Solicitantes.Add(solicitante);
                _dbContext.SaveChanges();
            }
            solicitante = _dbContext.Solicitantes.First(s => s.Rut == solicitante.Rut &&
                                                             s.Nombres == solicitante.Nombres &&
                                                             s.Apellidos == solicitante.Apellidos &&
                                                             s.Email == solicitante.Email &&
                                                             s.Fono == solicitante.Fono);
            var now = DateTime.Now;
            DateTime FechaTermino = now.AddDays(_dbContext.DefinicionProcesos.FirstOrDefault(q => q.DefinicionProcesoId == 1127).Duracion);
            Organizacion organizacion = new Organizacion();
            try
            {
                organizacion = _dbContext.Organizaciones.Where(o =>
                    o.TipoOrganizacionId == 1 &&
                    o.RubroId == mensajeOrganizacionesRES.ObjetoSocial.Rubro &&
                    o.SubRubroId == mensajeOrganizacionesRES.ObjetoSocial.SubRubroEspecifico &&
                    o.RazonSocial == mensajeOrganizacionesRES.NombreCooperativa.RazonSocial &&
                    o.Sigla == mensajeOrganizacionesRES.NombreCooperativa.NombreFantasiaOSigla &&
                    o.RegionId == mensajeOrganizacionesRES.DireccionDeLaCooperativa.Region &&
                    o.ComunaId == mensajeOrganizacionesRES.DireccionDeLaCooperativa.Comuna &&
                    o.Direccion == mensajeOrganizacionesRES.DireccionDeLaCooperativa.Direccion &&
                    o.Email == mensajeOrganizacionesRES.ContactoDeLaCooperativa.EMail &&
                    o.Fono == mensajeOrganizacionesRES.ContactoDeLaCooperativa.Telefono &&
                    o.URL == mensajeOrganizacionesRES.ContactoDeLaCooperativa.PaginaWeb &&
                    o.EsGeneroFemenino == (mensajeOrganizacionesRES.OtrosAcuerdos.ExclusivaMujeres == 1) &&
                    o.FechaCelebracion == DateTime.ParseExact(mensajeOrganizacionesRES.DatosDelSistema.FechaCelebracion, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) &&
                    o.NumeroSocios == mensajeOrganizacionesRES.DatosDelSistema.NumeroTotalSocios &&
                    o.NumeroSociosHombres == mensajeOrganizacionesRES.DatosDelSistema.NumeroSociosHombres &&
                    o.NumeroSociosMujeres == mensajeOrganizacionesRES.DatosDelSistema.NumeroSociasMujeres &&
                    o.EstadoId == 6 && // rol asignado
                    o.NumeroSociosConstituyentes == 0 &&//
                    o.EsImportanciaEconomica == false) //valores obligatorios de origanizacion, no vienen en el mensaje RES
                .OrderByDescending(q => q.OrganizacionId).First();
            }
            catch (Exception ex)
            {
                return BadRequest("{\"message\":\"organizacion no existe\"}");
            }

            Proceso proceso = new Proceso()
            {
                OrganizacionId = organizacion.OrganizacionId,
                SolicitanteId = solicitante.SolicitanteId,
                DefinicionProcesoId = 1127,
                FechaCreacion = now,
                FechaVencimiento = FechaTermino,
                Terminada = false
            };
            _dbContext.Procesos.Add(proceso);
            _dbContext.SaveChanges();
            proceso = _dbContext.Procesos.Where(q => q.OrganizacionId == organizacion.OrganizacionId).OrderBy(q => q.ProcesoId).Last();

            Workflow workflow = new Workflow()
            {
                ProcesoId = proceso.ProcesoId,
                FechaCreacion = now,
                FechaTermino = FechaTermino,
                DefinicionWorkflowId = 1231,
                UserId = "980c4a71-7d34-4fc7-89bd-012b3e41590f",
                TipoAprobacionId = 1
            };
            workflow = _dbContext.Workflows.Where(q => q.ProcesoId == proceso.ProcesoId).FirstOrDefault();

            List<Documento> documentos = new List<Documento>();
            documentos.Add(new Documento()
            {
                Url = mensajeOrganizacionesRES.Documentos.PublicacionDiarioOficial,
                Activo = true,
                OrganizacionId = organizacion.OrganizacionId,
                ProcesoId = proceso.ProcesoId,
                WorkflowId = workflow.WorkflowId,
                FechaCreacion = now.AddDays(_dbContext.DefinicionProcesos.FirstOrDefault(q => q.DefinicionProcesoId == 1127).Duracion),
                //FechaValidoHasta = FechaTermino,
                //Descripcion = "Publicacion del diario oficial" 
                Enviado = false,
                //TipoDocumentoCodigo = 
            });
            documentos.Add(new Documento()
            {
                Url = mensajeOrganizacionesRES.Documentos.PublicacionDiarioOficial,
                Activo = true,
                OrganizacionId = organizacion.OrganizacionId,
                ProcesoId = proceso.ProcesoId,
                WorkflowId = workflow.WorkflowId,
                FechaCreacion = now.AddDays(_dbContext.DefinicionProcesos.FirstOrDefault(q => q.DefinicionProcesoId == 1127).Duracion),
                //FechaValidoHasta = FechaTermino,
                //Descripcion = "Publicacion del diario oficial" 
                Enviado = false,
                //TipoDocumentoCodigo = 
            });
            documentos.Add(new Documento()
            {
                Url = mensajeOrganizacionesRES.Documentos.PublicacionDiarioOficial,
                Activo = true,
                OrganizacionId = organizacion.OrganizacionId,
                ProcesoId = proceso.ProcesoId,
                WorkflowId = workflow.WorkflowId,
                FechaCreacion = now.AddDays(_dbContext.DefinicionProcesos.FirstOrDefault(q => q.DefinicionProcesoId == 1127).Duracion),
                //FechaValidoHasta = FechaTermino,
                //Descripcion = "Publicacion del diario oficial" 
                Enviado = false,
                //TipoDocumentoCodigo = 
            });
            foreach (string otroDocumento in mensajeOrganizacionesRES.Documentos.OtrosDocumentos)
            {
                documentos.Add(new Documento()
                {
                    Url = otroDocumento,
                    Activo = true,
                    OrganizacionId = organizacion.OrganizacionId,
                    ProcesoId = proceso.ProcesoId,
                    WorkflowId = workflow.WorkflowId,
                    FechaCreacion = now.AddDays(_dbContext.DefinicionProcesos.FirstOrDefault(q => q.DefinicionProcesoId == 1127).Duracion),
                    //FechaValidoHasta = FechaTermino, 
                    Enviado = false 
                });
            }
            foreach (Documento documento in documentos)
            {
                _dbContext.Procesos.Add(proceso);
            }_dbContext.SaveChanges();

            return Ok($"{{\"ProcesoId\": \"{proceso.ProcesoId}\"}}");
        }
    }
}
