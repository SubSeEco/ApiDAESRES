using System.Text.Json;
using NJsonSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NJsonSchema.Validation;
using RES.API.BackOffice;
using DAES.API.BackOffice.Modelos;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroOrganizacionBOController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly MyDbContext _dbContext;

        public RegistroOrganizacionBOController(IMemoryCache cache, MyDbContext dbContext)
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
                Console.WriteLine("schema");
                var schema = JsonSchema.FromJsonAsync(organizacionRESSchema).Result;
                var validator = new JsonSchemaValidator();
                Console.WriteLine("valid");
                var validation = validator.Validate(jsonDocument.RootElement.ToString(), schema);
                if (validation.Count != 0) { return BadRequest("json invalido"); }
                Console.WriteLine("doc");
                MensajeOrganizacionRES registroOrganizacionBO = jsonDocument.Deserialize<MensajeOrganizacionRES>();
                Console.WriteLine("org");
                Organizacion org = new Organizacion
                {
                    TipoOrganizacionId = 1,
                    RubroId = registroOrganizacionBO.ObjetoSocial.Rubro,
                    SubRubroId = registroOrganizacionBO.ObjetoSocial.SubRubroEspecifico,
                    RazonSocial = registroOrganizacionBO.NombreCooperativa.RazonSocial,
                    Sigla = registroOrganizacionBO.NombreCooperativa.NombreFantasiaOSigla,
                    RegionId = registroOrganizacionBO.DireccionDeLaCooperativa.Region,
                    ComunaId = registroOrganizacionBO.DireccionDeLaCooperativa.Comuna,
                    Direccion = registroOrganizacionBO.DireccionDeLaCooperativa.Direccion,
                    Email = registroOrganizacionBO.ContactoDeLaCooperativa.EMail,
                    Fono = registroOrganizacionBO.ContactoDeLaCooperativa.Telefono,
                    URL = registroOrganizacionBO.ContactoDeLaCooperativa.PaginaWeb,
                    EsGeneroFemenino = (registroOrganizacionBO.OtrosAcuerdos.ExclusivaMujeres == 1),
                    FechaCelebracion = DateTime.ParseExact(registroOrganizacionBO.DatosDelSistema.FechaCelebracion, "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture),
                    NumeroSocios = registroOrganizacionBO.DatosDelSistema.NumeroTotalSocios,
                    NumeroSociosHombres = registroOrganizacionBO.DatosDelSistema.NumeroSociosHombres,
                    NumeroSociosMujeres = registroOrganizacionBO.DatosDelSistema.NumeroSociasMujeres,
                    EstadoId = 2,                  //
                    NumeroSociosConstituyentes = 0,//
                    EsImportanciaEconomica = false //valores obligatorios de origanizacion, no vienen en el mensaje RES
                };
                _dbContext.Organizacion.Add(org);
                _dbContext.SaveChanges();
                return Ok(registroOrganizacionBO.ContactoDeLaCooperativa.EMail);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
