﻿using System.Text.Json;
using NJsonSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NJsonSchema.Validation;
using RES.API.BackOffice;
using DAES.API.BackOffice.Modelos;

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
            try
            {
                _cache.TryGetValue("MensajeOrganizacionRES", out string organizacionRESSchema);
                var schema = JsonSchema.FromJsonAsync(organizacionRESSchema).Result;
                var validator = new JsonSchemaValidator();
                var validation = validator.Validate(jsonDocument.RootElement.ToString(), schema);
                if (validation.Count != 0) { return BadRequest("json invalido"); }

                MensajeOrganizacionRES creacionProcesoRES = jsonDocument.Deserialize<MensajeOrganizacionRES>();

                return Ok(creacionProcesoRES.ContactoDeLaCooperativa.EMail);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}