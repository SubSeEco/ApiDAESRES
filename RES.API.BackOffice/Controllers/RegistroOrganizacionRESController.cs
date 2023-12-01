using System.Text.Json;
using NJsonSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NJsonSchema.Validation;
using RES.API.BackOffice;
using DAES.API.BackOffice.Modelos;
using Microsoft.EntityFrameworkCore;
using Enum = DAES.API.BackOffice.Modelos.Enum;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroOrganizacionRESController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly MyDbContext _dbContext;

        public RegistroOrganizacionRESController(IMemoryCache cache, MyDbContext dbContext)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        // POST api/RegistroOrganizacionBO
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] JsonDocument jsonDocument)
        {
            try
            {
                _cache.TryGetValue("MensajeOrganizacionRES", out string organizacionRESSchema);
                var schema = JsonSchema.FromJsonAsync(organizacionRESSchema).Result;
                var validator = new JsonSchemaValidator();
                var validation = validator.Validate(jsonDocument.RootElement.ToString(), schema);
                if (validation.Count != 0) { return BadRequest("json invalido"); }
                MensajeOrganizacionRES mensajeOrganizacionesRES = jsonDocument.Deserialize<MensajeOrganizacionRES>();
                Organizacion org = new Organizacion
                {
                    TipoOrganizacionId = (int)Enum.TipoOrganizacion.Cooperativa,
                    SituacionId = (int)Enum.Situacion.Inactiva, 
                    RubroId = mensajeOrganizacionesRES.ObjetoSocial.Rubro,
                    SubRubroId = mensajeOrganizacionesRES.ObjetoSocial.SubRubroEspecifico,
                    RazonSocial = mensajeOrganizacionesRES.NombreCooperativa.RazonSocial,
                    Sigla = mensajeOrganizacionesRES.NombreCooperativa.NombreFantasiaOSigla,
                    RegionId = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Region,
                    ComunaId = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Comuna,
                    Direccion = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Direccion,
                    Email = mensajeOrganizacionesRES.ContactoDeLaCooperativa.EMail,
                    Fono = mensajeOrganizacionesRES.ContactoDeLaCooperativa.Telefono,
                    URL = mensajeOrganizacionesRES.ContactoDeLaCooperativa.PaginaWeb,
                    EsGeneroFemenino = (mensajeOrganizacionesRES.OtrosAcuerdos.ExclusivaMujeres == 1), // enum en contrato 1: verdadero, 0: falso
                    FechaCelebracion = DateTime.ParseExact(mensajeOrganizacionesRES.DatosDelSistema.FechaCelebracion, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    NumeroSocios = mensajeOrganizacionesRES.DatosDelSistema.NumeroTotalSocios,
                    NumeroSociosHombres = mensajeOrganizacionesRES.DatosDelSistema.NumeroSociosHombres,
                    NumeroSociosMujeres = mensajeOrganizacionesRES.DatosDelSistema.NumeroSociasMujeres,
                    EstadoId = (int)Enum.Estado.EnConstitucion,
                    NumeroSociosConstituyentes = 0, //
                    EsImportanciaEconomica = false //valores obligatorios de origanizacion, no vienen en el mensaje RES
                };
                _dbContext.Organizaciones.Add(org);
                _dbContext.SaveChanges();
                Organizacion organizacion = _dbContext.Organizaciones.Where(o =>
                    o.TipoOrganizacionId == (int)Enum.TipoOrganizacion.Cooperativa &&
                    o.SituacionId == (int)Enum.Situacion.Inactiva &&
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
                    o.EstadoId == (int)Enum.Estado.EnConstitucion &&
                    o.NumeroSociosConstituyentes == 0 &&
                    o.EsImportanciaEconomica == false
                ).OrderByDescending(q => q.OrganizacionId).First();


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
                DateTime FechaTermino = now.AddDays(_dbContext.DefinicionProcesos.FirstOrDefault(q => q.DefinicionProcesoId == (int)Enum.DefinicionProceso.InscripcionConstitucionRES).Duracion);


                Proceso proceso = new Proceso()
                {
                    OrganizacionId = organizacion.OrganizacionId,
                    SolicitanteId = solicitante.SolicitanteId,
                    DefinicionProcesoId = (int)Enum.DefinicionProceso.InscripcionConstitucionRES,
                    FechaCreacion = now,
                    FechaVencimiento = FechaTermino,
                    Terminada = false,
                    Creador = "ApiRes"
                };
                _dbContext.Procesos.Add(proceso);
                _dbContext.SaveChanges();
                proceso = _dbContext.Procesos.Where(q => q.OrganizacionId == organizacion.OrganizacionId).OrderBy(q => q.ProcesoId).Last();
                //aqui asignamos la persona con el perfil perfilAPI y que este sin asignacion por el momento 
                var persona = _dbContext.NetUsers.FirstOrDefault(q => q.PerfilId == (int)Enum.Perfil.perfilAPI && q.TareaAsignadaApi == false);
                if (persona == null)
                {
                    var personas = _dbContext.NetUsers.Where(q => q.PerfilId == (int)Enum.Perfil.perfilAPI && q.TareaAsignadaApi == true);
                    foreach (var p in personas)
                    {
                        p.TareaAsignadaApi = false;

                    }
                    _dbContext.SaveChanges();
                    persona = _dbContext.NetUsers.FirstOrDefault(q => q.PerfilId == (int)Enum.Perfil.perfilAPI && q.TareaAsignadaApi == false);
                }
                persona.TareaAsignadaApi = true;


                Workflow workflow = new Workflow()
                {
                    ProcesoId = proceso.ProcesoId,
                    FechaCreacion = now,
                    FechaTermino = FechaTermino,
                    DefinicionWorkflowId = (int)Enum.DefinicionWorkflow.InscripcionConstitucionRES,
                    UserId = persona.Id,
                    TipoAprobacionId = (int)Enum.TipoAprobacion.SinAprobacion
                };


                _dbContext.Add(workflow);
                _dbContext.SaveChanges();
                List<Documento> documentos = new List<Documento>();
                documentos.Add(new Documento()
                {
                    Url = mensajeOrganizacionesRES.Documentos.PublicacionDiarioOficial,
                    Activo = true,
                    OrganizacionId = organizacion.OrganizacionId,
                    ProcesoId = proceso.ProcesoId,
                    WorkflowId = workflow.WorkflowId,
                    FechaCreacion = now,
                    //FechaValidoHasta = FechaTermino,
                    Descripcion = "Publicacion del diario oficial",
                    Enviado = false,
                    //TipoDocumentoCodigo = 
                });
                documentos.Add(new Documento()
                {
                    Url = mensajeOrganizacionesRES.Documentos.InscripcionExtractoCBR,
                    Activo = true,
                    OrganizacionId = organizacion.OrganizacionId,
                    ProcesoId = proceso.ProcesoId,
                    WorkflowId = workflow.WorkflowId,
                    FechaCreacion = now,
                    //FechaValidoHasta = FechaTermino,
                    Descripcion = "Inscripcion extracto CBR",
                    Enviado = false,
                    //TipoDocumentoCodigo = 
                });
                documentos.Add(new Documento()
                {
                    Url = mensajeOrganizacionesRES.Documentos.EscrituraPublicaConstitucion,
                    Activo = true,
                    OrganizacionId = organizacion.OrganizacionId,
                    ProcesoId = proceso.ProcesoId,
                    WorkflowId = workflow.WorkflowId,
                    FechaCreacion = now,
                    //FechaValidoHasta = FechaTermino,
                    Descripcion = "Escritura publica de constitución",
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
                        FechaCreacion = now,
                        //FechaValidoHasta = FechaTermino, 
                        Enviado = false
                    });
                }
                foreach (Documento documento in documentos)
                {
                    _dbContext.Documentos.Add(documento);
                }
                _dbContext.SaveChanges();

                return Ok($"{{\"ProcesoId\": \"{proceso.ProcesoId}\"}}");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{{\"message\": \"error en actualizar sistema integrado\", \"entries\": \"{ex.Entries}\"}}");
            }
        }
    }
}
