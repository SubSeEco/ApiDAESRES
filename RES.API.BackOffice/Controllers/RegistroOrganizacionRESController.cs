using System.Text.Json;
using NJsonSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NJsonSchema.Validation;
using RES.API.BackOffice;
using DAES.API.BackOffice.Modelos;
using Microsoft.EntityFrameworkCore;
using Enum = DAES.API.BackOffice.Modelos.Enum;
using Microsoft.AspNetCore.Mvc.Filters;
using static App.API.ModulosRES;
using Documento = DAES.API.BackOffice.Modelos.Documento;


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
        [ServiceFilter(typeof(PostMethodFilter))]
        public async Task<IActionResult> Post([FromBody] JsonDocument jsonDocument)
        {
            try
            {
                _cache.TryGetValue("MensajeOrganizacionRES", out string organizacionRESSchema);
                var schema = JsonSchema.FromJsonAsync(organizacionRESSchema).Result;
                var validator = new JsonSchemaValidator();
                var validation = validator.Validate(jsonDocument.RootElement.ToString(), schema);
                if (validation.Count != 0) { return BadRequest(new { Message = "json invalido" }); }
                MensajeOrganizacionRES mensajeOrganizacionesRES = jsonDocument.Deserialize<MensajeOrganizacionRES>();


                //validamos que no se este re-procesando un mismo registro (caso de multiples envios de Solicitud desde RES)
                IQueryable<RESCrearOrgMensaje> MensajesAtencion = _dbContext.RESCrearOrgMensajes.Where(s => s.datosDelSistema_numeroDeAtencion == mensajeOrganizacionesRES.DatosDelSistema.NumeroDeAtencion);
                if (MensajesAtencion.Any())
                {
                    RESCrearOrgMensaje MensajeRepetido = MensajesAtencion.FirstOrDefault();

                    List<int> StoredActividades = new List<int>();
                    List<CooperadoYAdministrador> StoredCoopYAdms = new List<CooperadoYAdministrador>();
                    List<Calidad> StoredCalidades;
                    List<CapitalDelSocio> StoredCapitales;
                    List<Representante> StoredRepresentantes;
                    IQueryable<RESCrearOrgobjetoSocial_actividades> repActividades  = _dbContext.RESCrearOrgObjetoSocial_actividades.Where(s => s.RESCrearOrgMensajeId == MensajeRepetido.RESCrearOrgMensajeId);
                    foreach (RESCrearOrgobjetoSocial_actividades repAct in repActividades)
                    {
                        StoredActividades.Add(repAct.actividad);
                    }
                    IQueryable<RESCrearOrgCooperadosYAdministradores> repCoopYAdms = _dbContext.RESCrearOrgCooperadosYAdministradores.Where(s => s.RESCrearOrgMensajeId == MensajeRepetido.RESCrearOrgMensajeId);
                    foreach (RESCrearOrgCooperadosYAdministradores repCYA in repCoopYAdms)
                    {
                        StoredCalidades = new List<Calidad>();
                        StoredCapitales = new List<CapitalDelSocio>();
                        StoredRepresentantes = new List<Representante>();
                        IQueryable<RESCrearOrgCalidad> repCalidades = _dbContext.RESCrearOrgCalidades.Where(s => s.RESCrearOrgCooperadosYAdministradoresId == repCYA.RESCrearOrgCooperadosYAdministradoresId);
                        foreach (RESCrearOrgCalidad repCal in repCalidades)
                        {
                            StoredCalidades.Add(new Calidad()
                            {
                                TipoCalidades = repCal.Calidad
                            });
                        }
                        IQueryable<RESCrearOrgCapitalDelSocio> repCapitales = _dbContext.RESCrearOrgCapitalDelSocios.Where(s => s.RESCrearOrgCooperadosYAdministradoresId == repCYA.RESCrearOrgCooperadosYAdministradoresId);
                        foreach (RESCrearOrgCapitalDelSocio repCap in repCapitales)
                        {
                            StoredCapitales.Add(new CapitalDelSocio()
                            {
                                TiposAporte = repCap.tiposAporte,
                                CantidadCuotas = repCap.cantidadCuotas,
                                CapitalPagado = repCap.capitalPagado,
                                CapitalPorPagar = repCap.capitalPorPagar,
                                PlazoParaPagar = repCap.plazoParaPagar,
                                FormaEnQueSeraEnterado = repCap.formaEnQueSeraEnterado,
                                DescripcionAporte = repCap.descripcionAporte
                            });
                        }
                        IQueryable<RESCrearOrgRepresentante> repRepresentantes = _dbContext.RESCrearOrgRepresentantes.Where(s => s.RESCrearOrgCooperadosYAdministradoresId == repCYA.RESCrearOrgCooperadosYAdministradoresId);
                        foreach (RESCrearOrgRepresentante repRep in repRepresentantes)
                        {
                            StoredRepresentantes.Add(new Representante()
                            {
                                Rut = repRep.rut,
                                Dv = repRep.dv,
                                RutDV = repRep.rutDV,
                                PrimerApellido = repRep.primerApellido,
                                SegundoApellido = repRep.segundoApellido,
                                Nombres = repRep.nombres
                            });
                        }
                        StoredCoopYAdms.Add(new CooperadoYAdministrador()
                        {
                            Rut = repCYA.rut,
                            DV = repCYA.dv,
                            RutDV = repCYA.rutDV,
                            PrimerApellido = repCYA.primerApellido,
                            SegundoApellido = repCYA.segundoApellido,
                            Nombres = repCYA.nombres,
                            RazonSocial = repCYA.razonSocial,
                            Calle = repCYA.calle,
                            Numero = repCYA.numero,
                            Bloque = repCYA.bloque,
                            Departamento = repCYA.departamento,
                            VillaPoblacion = repCYA.villaPoblacion,
                            Region = repCYA.region,
                            Comuna = repCYA.comuna,
                            Direccion = repCYA.direccion,
                            Telefono = repCYA.telefono,
                            EMail = repCYA.eMail,
                            AdjuntarDocumento = new ModulosRES.Documento() { NombreArchivo = repCYA.adjuntarDocumento_nombreArchivo, URL = repCYA.adjuntarDocumento_url},
                            Representante = StoredRepresentantes,
                            Calidad = StoredCalidades,
                            CapitalDelSocio = StoredCapitales
                        });
                    }

                    List<ModulosRES.Documento> repOtrosDocs = new List<ModulosRES.Documento>();

                    ModulosRES.Documentos StoredDocumentos = new ModulosRES.Documentos()
                    {
                        EscrituraPublicaConstitucion = new ModulosRES.Documento() { NombreArchivo = MensajeRepetido.documentos_escrituraPublicaConstitucion_nombreArchivo,  URL = MensajeRepetido.documentos_escrituraPublicaConstitucion_url },
                        InscripcionExtractoCBR = new ModulosRES.Documento() { NombreArchivo = MensajeRepetido.documentos_inscripcionExtractoCBR_nombreArchivo, URL = MensajeRepetido.documentos_inscripcionExtractoCBR_url },
                        PublicacionDiarioOficial = new ModulosRES.Documento() { NombreArchivo = MensajeRepetido.documentos_publicacionDiarioOficial_nombreArchivo, URL = MensajeRepetido.documentos_publicacionDiarioOficial_url },
                        OtrosDocumentos = repOtrosDocs
                    };

                    return Conflict(new { Message = $"Solicitud de registro de organización repetida (numero de atención de mensaje RES ya existe)", numeroDeAtencion = MensajeRepetido.datosDelSistema_numeroDeAtencion, ProcesoId = MensajeRepetido.ProcesoId, ReceivedData = mensajeOrganizacionesRES
                    , StoredData = new MensajeOrganizacionRES()
                    {
                        ObjetoSocial = new ObjetoSocial() { Rubro=MensajeRepetido.objetoSocial_rubro,SubRubroEspecifico= MensajeRepetido.objetoSocial_subRubroEspecifico, Actividades = StoredActividades },
                        NombreCooperativa = new NombreCooperativa() { RazonSocial = MensajeRepetido.nombreCooperativa_razonSocial, NombreFantasiaOSigla = MensajeRepetido.nombreCooperativa_nombreFantasiaOSigla },
                        DireccionDeLaCooperativa = new DireccionDeLaCooperativa() { Calle = MensajeRepetido.direccionDeLaCooperativa_calle, Numero = MensajeRepetido.direccionDeLaCooperativa_numero, Bloque = MensajeRepetido.direccionDeLaCooperativa_bloque, Departamento = MensajeRepetido.direccionDeLaCooperativa_departamento
                        , VillaPoblacion = MensajeRepetido.direccionDeLaCooperativa_villaPoblacion, Region = MensajeRepetido.direccionDeLaCooperativa_region, Comuna = MensajeRepetido.direccionDeLaCooperativa_comuna, Direccion = MensajeRepetido.direccionDeLaCooperativa_direccion },
                        ContactoDeLaCooperativa = new ContactoDeLaCooperativa() { EMail = MensajeRepetido.contactoDeLaCooperativa_eMail, Telefono = MensajeRepetido.contactoDeLaCooperativa_telefono, PaginaWeb = MensajeRepetido.contactoDeLaCooperativa_paginaWeb },
                        CapitalDeLaCooperativa = new CapitalDeLaCooperativa() { MontoTotalCapitalInicial = MensajeRepetido.capitalDeLaCooperativa_montoTotalCapitalInicial, NumeroCuotasParticipacion = MensajeRepetido.capitalDeLaCooperativa_numeroCuotasParticipacion, ValorCuotasPaticipacion = MensajeRepetido.capitalDeLaCooperativa_valorCuotasPaticipacion },
                        DuracionDeLaCooperativa = new DuracionDeLaCooperativa() { Indefinido = MensajeRepetido.duracionDeLaCooperativa_indefinido, Anhos = MensajeRepetido.duracionDeLaCooperativa_anhos },
                        AdministracionDeLaCooperativa = new AdministracionDeLaCooperativa() { TipoAdministracion = MensajeRepetido.administracionDeLaCooperativa_tipoAdministracion },
                        ConsejoAdministracion = new ConsejoAdministracion() { NumeroMiembros = MensajeRepetido.consejoAdministracion_numeroMiembros, PlazoDuracion = MensajeRepetido.consejoAdministracion_plazoDuracion, DerechoReeleccion = MensajeRepetido.consejoAdministracion_derechoReeleccion },
                        JuntaDeVigilancia = new JuntaDeVigilancia() { NumeroMiembros = MensajeRepetido.juntaDeVigilancia_numeroMiembros, PlazoDuracion = MensajeRepetido.juntaDeVigilancia_plazoDuracion, DerechoReeleccion = MensajeRepetido.juntaDeVigilancia_derechoReeleccion },
                        DerechosYObligacionesSocios = new DerechosYObligacionesSocios() { AporteParaIngresoACooperativa = MensajeRepetido.derechosYObligacionesSocios_aporteParaIngresoACooperativa },
                        OtrosAcuerdos = new OtrosAcuerdos() { Inclusiva = MensajeRepetido.otrosAcuerdos_inclusiva, ExclusivaMujeres = MensajeRepetido.otrosAcuerdos_exclusivaMujeres, CalidadIndigena = MensajeRepetido.otrosAcuerdos_calidadIndigena },
                        CooperadosYAdministradores = StoredCoopYAdms,
                        Documentos = StoredDocumentos,
                        DatosDelSistema = new DatosDelSistema() { NumeroDeAtencion = MensajeRepetido.datosDelSistema_numeroDeAtencion, NumeroTotalSocios = MensajeRepetido.datosDelSistema_numeroTotalSocios, NumeroSociosHombres = MensajeRepetido.datosDelSistema_numeroSociosHombres, NumeroSociasMujeres = MensajeRepetido.datosDelSistema_numeroSociasMujeres
                        , RutSolicitante = MensajeRepetido.datosDelSistema_rutSolicitante, Dv = MensajeRepetido.datosDelSistema_dv, RutDV = MensajeRepetido.datosDelSistema_rutDV, NombresSolicitante = MensajeRepetido.datosDelSistema_nombresSolicitante, PrimerApellido = MensajeRepetido.datosDelSistema_primerApellido
                        , SegundoApellido = MensajeRepetido.datosDelSistema_segundoApellido, EmailSolicitante = MensajeRepetido.datosDelSistema_emailSolicitante, FonoSolicitante = MensajeRepetido.datosDelSistema_fonoSolicitante, FechaCelebracion = MensajeRepetido.datosDelSistema_fechaCelebracion }
                    }
                    });
                }


                    //validamos los codigos (llaves foraneas de tablas DAES) enviadas en el mensaje RES
                    bool ValidacionCodigos = true;
                List<string> errors = new List<string>();

                //subrubro-rubro
                IQueryable<DiccionarioSubRubro> diccionarioSubRubros = _dbContext.DiccionarioSubRubros.Where(s => s.CodigoRES == mensajeOrganizacionesRES.ObjetoSocial.SubRubroEspecifico);
                if (!diccionarioSubRubros.Any())
                {
                    ValidacionCodigos = false;
                    errors.Add($"Codigo RES SubRubro {mensajeOrganizacionesRES.ObjetoSocial.SubRubroEspecifico} no se encuentra en el diccionario");
                }

                IQueryable<DiccionarioRubro> diccionarioRubros = _dbContext.DiccionarioRubros.Where(s => s.CodigoRES == mensajeOrganizacionesRES.ObjetoSocial.Rubro);
                if (!diccionarioRubros.Any())
                {
                    ValidacionCodigos = false;
                    errors.Add($"Codigo RES Rubro {mensajeOrganizacionesRES.ObjetoSocial.Rubro} no se encuentra en el diccionario");
                }

                if (diccionarioRubros.Any() && diccionarioSubRubros.Any())
                {
                    IQueryable<SubRubro> SubRubro_RubroId = _dbContext.SubRubros.Where(s => s.SubRubroId == diccionarioSubRubros.FirstOrDefault().SubRubroId &&
                                                                    s.RubroId == diccionarioRubros.FirstOrDefault().RubroId);
                    if (!SubRubro_RubroId.Any())
                    {
                        ValidacionCodigos = false;
                        errors.Add($"Combinacion codigos SubRubroId {diccionarioSubRubros.FirstOrDefault().SubRubroId} y RubroId {diccionarioRubros.FirstOrDefault().RubroId} no se encuentra en tabla SubRubros " +
                                   $"(codigos RES correspondientes: {mensajeOrganizacionesRES.ObjetoSocial.SubRubroEspecifico} y {mensajeOrganizacionesRES.ObjetoSocial.Rubro})");
                    }
                }

                //comuna-region
                IQueryable<DiccionarioComuna> diccionarioComunas = _dbContext.DiccionarioComunas.Where(s => s.CodigoRES == mensajeOrganizacionesRES.DireccionDeLaCooperativa.Comuna);
                if (!diccionarioComunas.Any())
                {
                    ValidacionCodigos = false;
                    errors.Add($"Codigo RES Comuna {mensajeOrganizacionesRES.DireccionDeLaCooperativa.Comuna} no se encuentra en el diccionario");
                }

                IQueryable<DiccionarioRegion> diccionarioRegiones = _dbContext.DiccionarioRegiones.Where(s => s.CodigoRES == mensajeOrganizacionesRES.DireccionDeLaCooperativa.Region);
                if (!diccionarioRegiones.Any())
                {
                    ValidacionCodigos = false;
                    errors.Add($"Codigo RES Region {mensajeOrganizacionesRES.DireccionDeLaCooperativa.Region} no se encuentra en el diccionario");
                }

                if (diccionarioRegiones.Any() && diccionarioComunas.Any())
                {
                    IQueryable<Comuna> Comunas_RegionId = _dbContext.Comunas.Where(s => s.ComunaId == diccionarioComunas.FirstOrDefault().ComunaId &&
                                                                                        s.RegionId == diccionarioRegiones.FirstOrDefault().RegionId);
                    if (!Comunas_RegionId.Any())
                    {
                        ValidacionCodigos = false;
                        errors.Add($"Combinacion codigos ComunaId {diccionarioComunas.FirstOrDefault().ComunaId} y RegionId {diccionarioRegiones.FirstOrDefault().RegionId} no se encuentra en tabla Comunas " +
                                   $"(codigos RES correspondientes: {mensajeOrganizacionesRES.DireccionDeLaCooperativa.Comuna} y {mensajeOrganizacionesRES.DireccionDeLaCooperativa.Region})");
                    }
                }


                if (!ValidacionCodigos) {
                    return BadRequest(new { Message = "errores: "+string.Join(", ", errors) });
                }

                //recuperamos el definicionWorkflowId que usaremos para ContitucionWeb de cooperativa digital RES
                IQueryable<DefinicionWorkflow> definicionWorkflow = _dbContext.DefinicionWorkflows.Where(s => s.Habilitado && s.DefinicionProcesoId == (int)Enum.DefinicionProceso.InscripcionConstitucionCooperativaDigital
                    && s.TipoWorkflowId == (int)Enum.TipoWorkflow.VerificarDatosActualizacion).OrderBy(s => s.Secuencia).ThenBy(s => s.DefinicionWorkflowId);

                /*
                if (!definicionWorkflow.Any())
                {
                }
                */

                //guardamos mensaje RES en backoffice
                RESCrearOrgMensaje RESMensaje = new RESCrearOrgMensaje()
                {
                    objetoSocial_rubro = mensajeOrganizacionesRES.ObjetoSocial.Rubro,
                    objetoSocial_subRubroEspecifico = mensajeOrganizacionesRES.ObjetoSocial.SubRubroEspecifico,
                    nombreCooperativa_razonSocial = mensajeOrganizacionesRES.NombreCooperativa.RazonSocial,
                    nombreCooperativa_nombreFantasiaOSigla = mensajeOrganizacionesRES.NombreCooperativa.NombreFantasiaOSigla,
                    direccionDeLaCooperativa_calle = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Calle,
                    direccionDeLaCooperativa_numero = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Numero,
                    direccionDeLaCooperativa_bloque = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Bloque,
                    direccionDeLaCooperativa_departamento = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Departamento,
                    direccionDeLaCooperativa_villaPoblacion = mensajeOrganizacionesRES.DireccionDeLaCooperativa.VillaPoblacion,
                    direccionDeLaCooperativa_region = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Region,
                    direccionDeLaCooperativa_comuna = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Comuna,
                    direccionDeLaCooperativa_direccion = mensajeOrganizacionesRES.DireccionDeLaCooperativa.Direccion,
                    contactoDeLaCooperativa_eMail = mensajeOrganizacionesRES.ContactoDeLaCooperativa.EMail,
                    contactoDeLaCooperativa_telefono = mensajeOrganizacionesRES.ContactoDeLaCooperativa.Telefono,
                    contactoDeLaCooperativa_paginaWeb = mensajeOrganizacionesRES.ContactoDeLaCooperativa.PaginaWeb,
                    capitalDeLaCooperativa_montoTotalCapitalInicial = mensajeOrganizacionesRES.CapitalDeLaCooperativa.MontoTotalCapitalInicial,
                    capitalDeLaCooperativa_numeroCuotasParticipacion = mensajeOrganizacionesRES.CapitalDeLaCooperativa.NumeroCuotasParticipacion,
                    capitalDeLaCooperativa_valorCuotasPaticipacion = mensajeOrganizacionesRES.CapitalDeLaCooperativa.ValorCuotasPaticipacion,
                    duracionDeLaCooperativa_indefinido = mensajeOrganizacionesRES.DuracionDeLaCooperativa.Indefinido,
                    duracionDeLaCooperativa_anhos = mensajeOrganizacionesRES.DuracionDeLaCooperativa.Anhos,
                    administracionDeLaCooperativa_tipoAdministracion = mensajeOrganizacionesRES.AdministracionDeLaCooperativa.TipoAdministracion,
                    consejoAdministracion_numeroMiembros = mensajeOrganizacionesRES.ConsejoAdministracion.NumeroMiembros,
                    consejoAdministracion_plazoDuracion = mensajeOrganizacionesRES.ConsejoAdministracion.PlazoDuracion,
                    consejoAdministracion_derechoReeleccion = mensajeOrganizacionesRES.ConsejoAdministracion.DerechoReeleccion,
                    juntaDeVigilancia_numeroMiembros = mensajeOrganizacionesRES.JuntaDeVigilancia.NumeroMiembros,
                    juntaDeVigilancia_plazoDuracion = mensajeOrganizacionesRES.JuntaDeVigilancia.PlazoDuracion,
                    juntaDeVigilancia_derechoReeleccion = mensajeOrganizacionesRES.JuntaDeVigilancia.DerechoReeleccion,
                    derechosYObligacionesSocios_aporteParaIngresoACooperativa = mensajeOrganizacionesRES.DerechosYObligacionesSocios.AporteParaIngresoACooperativa,
                    otrosAcuerdos_inclusiva = mensajeOrganizacionesRES.OtrosAcuerdos.Inclusiva,
                    otrosAcuerdos_exclusivaMujeres = mensajeOrganizacionesRES.OtrosAcuerdos.ExclusivaMujeres,
                    otrosAcuerdos_calidadIndigena = mensajeOrganizacionesRES.OtrosAcuerdos.CalidadIndigena,
                    documentos_escrituraPublicaConstitucion_url = mensajeOrganizacionesRES.Documentos.EscrituraPublicaConstitucion.URL,
                    documentos_escrituraPublicaConstitucion_nombreArchivo = mensajeOrganizacionesRES.Documentos.EscrituraPublicaConstitucion.NombreArchivo,
                    documentos_inscripcionExtractoCBR_url = mensajeOrganizacionesRES.Documentos.InscripcionExtractoCBR.URL,
                    documentos_inscripcionExtractoCBR_nombreArchivo = mensajeOrganizacionesRES.Documentos.InscripcionExtractoCBR.NombreArchivo,
                    documentos_publicacionDiarioOficial_url = mensajeOrganizacionesRES.Documentos.PublicacionDiarioOficial.URL,
                    documentos_publicacionDiarioOficial_nombreArchivo = mensajeOrganizacionesRES.Documentos.PublicacionDiarioOficial.NombreArchivo,
                    datosDelSistema_numeroDeAtencion = mensajeOrganizacionesRES.DatosDelSistema.NumeroDeAtencion,
                    datosDelSistema_numeroTotalSocios = mensajeOrganizacionesRES.DatosDelSistema.NumeroTotalSocios,
                    datosDelSistema_numeroSociosHombres = mensajeOrganizacionesRES.DatosDelSistema.NumeroSociosHombres,
                    datosDelSistema_numeroSociasMujeres = mensajeOrganizacionesRES.DatosDelSistema.NumeroSociasMujeres,
                    datosDelSistema_rutSolicitante = mensajeOrganizacionesRES.DatosDelSistema.RutSolicitante,
                    datosDelSistema_dv = mensajeOrganizacionesRES.DatosDelSistema.Dv,
                    datosDelSistema_rutDV = mensajeOrganizacionesRES.DatosDelSistema.RutDV,
                    datosDelSistema_nombresSolicitante = mensajeOrganizacionesRES.DatosDelSistema.NombresSolicitante,
                    datosDelSistema_primerApellido = mensajeOrganizacionesRES.DatosDelSistema.PrimerApellido,
                    datosDelSistema_segundoApellido = mensajeOrganizacionesRES.DatosDelSistema.SegundoApellido,
                    datosDelSistema_emailSolicitante = mensajeOrganizacionesRES.DatosDelSistema.EmailSolicitante,
                    datosDelSistema_fonoSolicitante = mensajeOrganizacionesRES.DatosDelSistema.FonoSolicitante,
                    datosDelSistema_fechaCelebracion = mensajeOrganizacionesRES.DatosDelSistema.FechaCelebracion
                };
                var id = RESMensaje.RESCrearOrgMensajeId;
                _dbContext.RESCrearOrgMensajes.Add(RESMensaje);
                _dbContext.SaveChanges();


                RESCrearOrgCooperadosYAdministradores RESCoopYAdm = new RESCrearOrgCooperadosYAdministradores();
                List<RESCrearOrgRepresentante> RESrepresentantes = new List<RESCrearOrgRepresentante>();
                List<RESCrearOrgCapitalDelSocio> RESCapitales = new List<RESCrearOrgCapitalDelSocio>();
                List<RESCrearOrgCalidad> RESCalidades = new List<RESCrearOrgCalidad>();
                foreach (CooperadoYAdministrador coopoadm in mensajeOrganizacionesRES.CooperadosYAdministradores)
                {
                    RESCoopYAdm = new RESCrearOrgCooperadosYAdministradores()
                    {
                        RESCrearOrgMensajeId = (int)RESMensaje.RESCrearOrgMensajeId,
                        rut = coopoadm.Rut,
                        dv = coopoadm.DV,
                        rutDV = coopoadm.RutDV,
                        primerApellido = coopoadm.PrimerApellido,
                        segundoApellido = coopoadm.SegundoApellido,
                        nombres = coopoadm.Nombres,
                        razonSocial = coopoadm.RazonSocial,
                        calle = coopoadm.Calle,
                        numero = coopoadm.Numero,
                        bloque = coopoadm.Bloque,
                        departamento = coopoadm.Departamento,
                        villaPoblacion = coopoadm.VillaPoblacion,
                        region = coopoadm.Region,
                        comuna = coopoadm.Comuna,
                        direccion = coopoadm.Direccion,
                        telefono = coopoadm.Telefono,
                        eMail = coopoadm.EMail,
                        adjuntarDocumento_url = coopoadm.AdjuntarDocumento.URL,
                        adjuntarDocumento_nombreArchivo = coopoadm.AdjuntarDocumento.NombreArchivo
                    };
                    _dbContext.RESCrearOrgCooperadosYAdministradores.Add(RESCoopYAdm);
                    _dbContext.SaveChanges();
                    foreach (Representante representante in coopoadm.Representante)
                    {
                        RESrepresentantes.Add(new RESCrearOrgRepresentante()
                        {
                            RESCrearOrgCooperadosYAdministradoresId = RESCoopYAdm.RESCrearOrgCooperadosYAdministradoresId,
                            rut = representante.Rut,
                            dv = representante.Dv,
                            rutDV = representante.RutDV,
                            primerApellido = representante.PrimerApellido,
                            segundoApellido = representante.SegundoApellido,
                            nombres = representante.Nombres,
                            adjuntarPoderRepresentacion_url = representante.AdjuntarPoderRepresentacion.URL,
                            adjuntarPoderRepresentacion_nombreArchivo = representante.AdjuntarPoderRepresentacion.NombreArchivo
                        });
                    }
                    foreach (Calidad calidad in coopoadm.Calidad)
                    {
                        RESCalidades.Add(new RESCrearOrgCalidad()
                        {
                            RESCrearOrgCooperadosYAdministradoresId = RESCoopYAdm.RESCrearOrgCooperadosYAdministradoresId,
                            Calidad = calidad.TipoCalidades
                        });
                    }
                    foreach (CapitalDelSocio capital in coopoadm.CapitalDelSocio)
                    {
                        RESCapitales.Add(new RESCrearOrgCapitalDelSocio()
                        {
                            RESCrearOrgCooperadosYAdministradoresId = RESCoopYAdm.RESCrearOrgCooperadosYAdministradoresId,
                            tiposAporte = capital.TiposAporte,
                            cantidadCuotas = capital.CantidadCuotas,
                            capitalPagado = capital.CapitalPagado,
                            capitalPorPagar = capital.CapitalPorPagar,
                            plazoParaPagar = capital.PlazoParaPagar,
                            formaEnQueSeraEnterado = capital.FormaEnQueSeraEnterado,
                            descripcionAporte = capital.DescripcionAporte
                        });
                    }
                }
                foreach (RESCrearOrgRepresentante RESrepresentante in RESrepresentantes)
                {
                    _dbContext.RESCrearOrgRepresentantes.Add(RESrepresentante);
                }
                foreach (RESCrearOrgCalidad REScalidad in RESCalidades)
                {
                    _dbContext.RESCrearOrgCalidades.Add(REScalidad);
                }
                foreach (RESCrearOrgCapitalDelSocio REScapital in RESCapitales)
                {
                    _dbContext.RESCrearOrgCapitalDelSocios.Add(REScapital);
                }

                foreach (int actividad in mensajeOrganizacionesRES.ObjetoSocial.Actividades)
                {
                    _dbContext.RESCrearOrgObjetoSocial_actividades.Add(new RESCrearOrgobjetoSocial_actividades()
                    {
                        RESCrearOrgMensajeId = (int)RESMensaje.RESCrearOrgMensajeId,
                        actividad = actividad
                    });
                }
                _dbContext.SaveChanges();

                // Guardamos el Proceso de registro organizacion en las tablas ya definidas; Proceso, Organizacion, Solicitante, Documento, Workflow
                Rol rol = new Rol();
                _dbContext.Roles.Add(rol);
                _dbContext.SaveChanges();

                Organizacion organizacion = new Organizacion
                {
                    TipoOrganizacionId = (int)Enum.TipoOrganizacion.Cooperativa,
                    NumeroRegistro = rol.RolId.ToString(),
                    SituacionId = (int)Enum.Situacion.Activa,
                    RubroId = diccionarioRubros.FirstOrDefault().RubroId,
                    SubRubroId = diccionarioSubRubros.FirstOrDefault().SubRubroId,
                    RazonSocial = mensajeOrganizacionesRES.NombreCooperativa.RazonSocial,
                    Sigla = mensajeOrganizacionesRES.NombreCooperativa.NombreFantasiaOSigla,
                    RegionId = diccionarioRegiones.FirstOrDefault().RegionId,
                    ComunaId = diccionarioComunas.FirstOrDefault().ComunaId,
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
                    EstadoId = (int)Enum.Estado.Vigente,
                    NumeroSociosConstituyentes = 0,//
                    EsImportanciaEconomica = false //valores obligatorios de origanizacion, no vienen en el mensaje RES
                };
                _dbContext.Organizaciones.Add(organizacion);
                _dbContext.SaveChanges();


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
                else
                {
                    solicitante = _dbContext.Solicitantes.First(s => s.Rut == solicitante.Rut &&
                                                                     s.Nombres == solicitante.Nombres &&
                                                                     s.Apellidos == solicitante.Apellidos &&
                                                                     s.Email == solicitante.Email &&
                                                                     s.Fono == solicitante.Fono);
                }


                var now = DateTime.Now;
                DateTime FechaTermino = now.AddDays(_dbContext.DefinicionProcesos.FirstOrDefault(q => q.DefinicionProcesoId == (int)Enum.DefinicionProceso.InscripcionConstitucionCooperativaDigital).Duracion);


                Proceso proceso = new Proceso()
                {
                    OrganizacionId = organizacion.OrganizacionId,//
                    SolicitanteId = solicitante.SolicitanteId,
                    DefinicionProcesoId = (int)Enum.DefinicionProceso.InscripcionConstitucionCooperativaDigital,
                    FechaCreacion = now,
                    FechaVencimiento = FechaTermino,
                    Terminada = false,
                    Creador = "ApiRes",
                    Observacion = "Documentos por subir - pendiente"
                };
                _dbContext.Procesos.Add(proceso);
                _dbContext.SaveChanges();
                RESMensaje.ProcesoId = proceso.ProcesoId;
                _dbContext.RESCrearOrgMensajes.Update(RESMensaje);

                //aqui asignamos la persona con el perfil perfilAPI y que este sin asignacion por el momento 
                var persona = _dbContext.NetUsers.FirstOrDefault(q => q.PerfilId == (int)Enum.Perfil.ConstitucionWeb && q.TareaAsignadaApi == false);
                if (persona == null)
                {
                    var personas = _dbContext.NetUsers.Where(q => q.PerfilId == (int)Enum.Perfil.ConstitucionWeb && q.TareaAsignadaApi == true);
                    foreach (var p in personas)
                    {
                        p.TareaAsignadaApi = false;

                    }
                    _dbContext.SaveChanges();
                    persona = _dbContext.NetUsers.FirstOrDefault(q => q.PerfilId == (int)Enum.Perfil.ConstitucionWeb && q.TareaAsignadaApi == false);
                }
                persona.TareaAsignadaApi = true;

                Workflow? primerWorkflow = null ;

                foreach (DefinicionWorkflow workflow in definicionWorkflow)
                {
                    Workflow newWorkflow = new Workflow()
                    {
                        ProcesoId = proceso.ProcesoId,
                        FechaCreacion = now,
                        FechaTermino = FechaTermino,
                        DefinicionWorkflowId = workflow.DefinicionWorkflowId,
                        UserId = persona.Id,
                        TipoAprobacionId = (int)Enum.TipoAprobacion.SinAprobacion
                    };
                    _dbContext.Add(newWorkflow);
                    if (primerWorkflow is null) { primerWorkflow = newWorkflow; }
                }
                _dbContext.SaveChanges();

                int? primerWorkflowId = null;
                if (primerWorkflow is not null) { primerWorkflowId = primerWorkflow.WorkflowId; }

                List<Documento> documentos = new List<Documento>();
                documentos.Add(new Documento()
                {
                    FileName = mensajeOrganizacionesRES.Documentos.PublicacionDiarioOficial.NombreArchivo,
                    Url = mensajeOrganizacionesRES.Documentos.PublicacionDiarioOficial.URL,
                    Activo = false,
                    OrganizacionId = organizacion.OrganizacionId,//
                    ProcesoId = proceso.ProcesoId,
                    WorkflowId = primerWorkflowId,
                    FechaCreacion = now,
                    //FechaValidoHasta = FechaTermino,
                    Descripcion = "Publicacion del diario oficial",
                    Enviado = false,
                    //TipoDocumentoCodigo = 
                });
                documentos.Add(new Documento()
                {
                    Url = mensajeOrganizacionesRES.Documentos.InscripcionExtractoCBR.URL,
                    FileName = mensajeOrganizacionesRES.Documentos.InscripcionExtractoCBR.NombreArchivo,
                    Activo = false,
                    OrganizacionId = organizacion.OrganizacionId,//
                    ProcesoId = proceso.ProcesoId,
                    WorkflowId = primerWorkflowId,
                    FechaCreacion = now,
                    //FechaValidoHasta = FechaTermino,
                    Descripcion = "Inscripcion extracto CBR",
                    Enviado = false,
                    //TipoDocumentoCodigo = 
                });
                documentos.Add(new Documento()
                {
                    Url = mensajeOrganizacionesRES.Documentos.EscrituraPublicaConstitucion.URL,
                    FileName = mensajeOrganizacionesRES.Documentos.EscrituraPublicaConstitucion.NombreArchivo,
                    Activo = false,
                    OrganizacionId = organizacion.OrganizacionId,//
                    ProcesoId = proceso.ProcesoId,
                    WorkflowId = primerWorkflowId,
                    FechaCreacion = now,
                    //FechaValidoHasta = FechaTermino,
                    Descripcion = "Escritura publica de constitución",
                    Enviado = false,
                    //TipoDocumentoCodigo = 
                });
                foreach (ModulosRES.Documento otroDocumento in mensajeOrganizacionesRES.Documentos.OtrosDocumentos)
                {
                    documentos.Add(new Documento()
                    {
                        Url = otroDocumento.URL,
                        FileName = otroDocumento.NombreArchivo,
                        Activo = false,
                        OrganizacionId = organizacion.OrganizacionId,//
                        ProcesoId = proceso.ProcesoId,
                        WorkflowId = primerWorkflowId,
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

                var resultData = new { Message = "La organización ha sido registrada exitosamente", ProcesoId = proceso.ProcesoId, ReceivedData = mensajeOrganizacionesRES };
                HttpContext.Items["ProcesoId"] = proceso.ProcesoId;
                HttpContext.Items["mensajeOrganizacionesRES"] = mensajeOrganizacionesRES;

                return Ok(resultData);
            }
            catch (DbUpdateException ex)
            {
                string entries = "[" + string.Join(", ", ex.Entries) + "]";
                return BadRequest($"{{\"message\": \"error en actualizar sistema integrado\", \"entries\": \"{entries}\"}}");
            }
        }

        // Action filter for POST method
        public class PostMethodFilter : IAsyncActionFilter
        {
            private readonly MyDbContext _dbContext;

            public PostMethodFilter(MyDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // Continue to the action method
                var resultContext = await next();
                // Confirmar que la organización haya sido registrada exitosamente
                if (context.HttpContext.Response.StatusCode == 200)
                {
                    if (context.HttpContext.Items.TryGetValue("ProcesoId", out var ProcesoIdObject) && ProcesoIdObject is int ProcesoId)
                    {
                        //descargamos de RES y subimos a backoffice los contenidos de los documentos contenidos en el mensaje
                        Proceso proceso = _dbContext.Procesos.FirstOrDefault(p => p.ProcesoId == ProcesoId);
                        proceso.Observacion = "Documentos por subir - en proceso";
                        _dbContext.Procesos.Update(proceso);
                        _dbContext.SaveChanges();

                        IQueryable<Documento> documentos = _dbContext.Documentos.Where(p => p.ProcesoId == ProcesoId);
                        //
                        int numeroDocumentosFallidos = 0;
                        foreach (Documento documento in documentos)
                        {
                            try
                            {
                                string urlArchivo = documento.Url;
                                byte[] archivoBytes = DescargarArchivo(urlArchivo);
                                documento.Content = archivoBytes;
                                documento.Activo = true;
                                _dbContext.Documentos.Update(documento);
                            }
                            catch (Exception ex)
                            {
                                numeroDocumentosFallidos++;
                            }
                        }
                        if (numeroDocumentosFallidos == documentos.Count())
                        {
                            proceso.Observacion = $"\"fallo la subida de todos los {numeroDocumentosFallidos} documentos";
                        }
                        else if (numeroDocumentosFallidos == 0)
                        {
                            proceso.Observacion = null;
                        }
                        else
                        {
                            proceso.Observacion = $"fallo la subida de {numeroDocumentosFallidos} documentos de un total de {documentos.Count()}";
                        }
                        _dbContext.Procesos.Update(proceso);
                        _dbContext.SaveChanges();
                    }
                }

            }

            private byte[] DescargarArchivo(string url)
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    return client.DownloadData(url);
                }
            }
        }
    }
}
