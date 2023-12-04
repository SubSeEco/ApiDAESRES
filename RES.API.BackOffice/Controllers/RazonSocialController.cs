using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DAES.API.BackOffice.Modelos; // Asegúrate de incluir el espacio de nombres de tus modelos

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazonSocialController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public RazonSocialController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/RazonSocial/{RazonSocial}
        [HttpGet("{RazonSocial}")]
        [Produces("application/json")]
        public IActionResult Get(string RazonSocial)
        {
            // Puedes realizar operaciones en la base de datos utilizando _dbContext
            var datos = _dbContext.Organizaciones.Where(q => q.RazonSocial == RazonSocial);

            if (datos.Any())
            {
                Organizacion organizacion = datos.Select(c => new Organizacion { OrganizacionId = c.OrganizacionId, EstadoId = c.EstadoId, NumeroRegistro = c.NumeroRegistro, RUT = c.RUT, RazonSocial = c.RazonSocial, Sigla = c.Sigla, Direccion = c.Direccion, Fono = c.Fono, Fax = c.Fax, Email = c.Email, URL = c.URL, NumeroSociosConstituyentes = c.NumeroSociosConstituyentes, NumeroPeronasJuridicas = c.NumeroPeronasJuridicas, NumeroSocios = c.NumeroSocios, NumeroSociosHombres = c.NumeroSociosHombres, NumeroSociosMujeres = c.NumeroSociosMujeres, MinistroDeFe = c.MinistroDeFe, EsGeneroFemenino = c.EsGeneroFemenino, CiudadAsamblea = c.CiudadAsamblea, NombreContacto = c.NombreContacto, DireccionContacto = c.DireccionContacto, TelefonoContacto = c.TelefonoContacto, EmailContacto = c.EmailContacto, FechaCreacion = c.FechaCreacion, FechaCelebracion = c.FechaCelebracion, FechaPubliccionDiarioOficial = c.FechaPubliccionDiarioOficial, FechaActualizacion = c.FechaActualizacion, EsImportanciaEconomica = c.EsImportanciaEconomica, FechaVigente = c.FechaVigente, FechaDisolucion = c.FechaDisolucion, FechaConstitucion = c.FechaConstitucion, FechaCancelacion = c.FechaCancelacion, FechaInexistencia = c.FechaInexistencia, FechaAsignacionRol = c.FechaAsignacionRol, SituacionId = c.SituacionId, fechaasambleadirectorio = c.fechaasambleadirectorio }).First();
                return Ok(JsonSerializer.Serialize(organizacion));
            }
            else {
                return NoContent();
            }
        }
    }
}