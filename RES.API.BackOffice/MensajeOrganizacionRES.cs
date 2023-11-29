using System.Text.Json.Serialization;
using MessagePack;
using Newtonsoft.Json;
using static App.API.ModulosRES;

namespace RES.API.BackOffice
{
    // MensajeOrganizacionRES GestionProcesosDeserializada = JsonSerializer.Deserialize<MensajeOrganizacionRES>(myJsonResponse);
    public class MensajeOrganizacionRES
    {
        [JsonPropertyName("objetoSocial")]
        public ObjetoSocial ObjetoSocial { get; set; }

        [JsonPropertyName("nombreCooperativa")]
        public NombreCooperativa NombreCooperativa { get; set; }

        [JsonPropertyName("direccionDeLaCooperativa")]
        public DireccionDeLaCooperativa DireccionDeLaCooperativa { get; set; }

        [JsonPropertyName("contactoDeLaCooperativa")]
        public ContactoDeLaCooperativa ContactoDeLaCooperativa { get; set; }

        [JsonPropertyName("capitalDeLaCooperativa")]
        public CapitalDeLaCooperativa CapitalDeLaCooperativa { get; set; }

        [JsonPropertyName("duracionDeLaCooperativa")]
        public DuracionDeLaCooperativa DuracionDeLaCooperativa { get; set; }

        [JsonPropertyName("administracionDeLaCooperativa")]
        public AdministracionDeLaCooperativa AdministracionDeLaCooperativa { get; set; }

        [JsonPropertyName("consejoAdministracion")]
        public ConsejoAdministracion ConsejoAdministracion { get; set; }

        [JsonPropertyName("juntaDeVigilancia")]
        public JuntaDeVigilancia JuntaDeVigilancia { get; set; }

        [JsonPropertyName("derechosYObligacionesSocios")]
        public DerechosYObligacionesSocios DerechosYObligacionesSocios { get; set; }

        [JsonPropertyName("otrosAcuerdos")]
        public OtrosAcuerdos OtrosAcuerdos { get; set; }

        [JsonPropertyName("cooperadosYAdministradores")]
        public List<CooperadoYAdministrador> CooperadosYAdministradores { get; set; }

        [JsonPropertyName("documentos")]
        public Documentos Documentos { get; set; }

        [JsonPropertyName("datosDelSistema")]
        public DatosDelSistema DatosDelSistema { get; set; }

        public bool NullParameter()
        {
            return NombreCooperativa is null || NombreCooperativa.NullParameter() ||
                    DireccionDeLaCooperativa is null || DireccionDeLaCooperativa.NullParameter() ||
                    ContactoDeLaCooperativa is null || ContactoDeLaCooperativa.NullParameter() ||
                    Documentos is null || Documentos.NullParameter() ||
                    DatosDelSistema is null || DatosDelSistema.NullParameter();
        }
    }
}
