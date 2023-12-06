using System.Text.Json.Serialization;

namespace App.API
{
    public class ModulosRES
    {
        public class AdministracionDeLaCooperativa
        {
            [JsonPropertyName("tipoAdministracion")]
            public int TipoAdministracion { get; set; }
        }

        public class Calidad
        {
            [JsonPropertyName("tipoCalidades")]
            public int TipoCalidades { get; set; }
        }

        public class CapitalDeLaCooperativa
        {
            [JsonPropertyName("montoTotalCapitalInicial")]
            public int MontoTotalCapitalInicial { get; set; }

            [JsonPropertyName("numeroCuotasParticipacion")]
            public int NumeroCuotasParticipacion { get; set; }

            [JsonPropertyName("valorCuotasPaticipacion")]
            public int ValorCuotasPaticipacion { get; set; }
        }

        public class CapitalDelSocio
        {
            [JsonPropertyName("tiposAporte")]
            public int TiposAporte { get; set; }

            [JsonPropertyName("cantidadCuotas")]
            public int CantidadCuotas { get; set; }

            [JsonPropertyName("capitalPagado")]
            public int CapitalPagado { get; set; }

            [JsonPropertyName("capitalPorPagar")]
            public int CapitalPorPagar { get; set; }

            [JsonPropertyName("plazoParaPagar")]
            public int PlazoParaPagar { get; set; }

            [JsonPropertyName("formaEnQueSeraEnterado")]
            public string FormaEnQueSeraEnterado { get; set; }

            [JsonPropertyName("descripcionAporte")]
            public string DescripcionAporte { get; set; }
        }

        public class ConsejoAdministracion
        {
            [JsonPropertyName("numeroMiembros")]
            public int NumeroMiembros { get; set; }

            [JsonPropertyName("plazoDuracion")]
            public int PlazoDuracion { get; set; }

            [JsonPropertyName("derechoReeleccion")]
            public int DerechoReeleccion { get; set; }
        }

        public class ContactoDeLaCooperativa
        {
            [JsonPropertyName("eMail")]
            public string EMail { get; set; }

            [JsonPropertyName("telefono")]
            public string Telefono { get; set; }

            [JsonPropertyName("paginaWeb")]
            public string PaginaWeb { get; set; }

            internal bool NullParameter()
            {
                return ((this.EMail is null) ||
                        (this.Telefono is null));
            }
        }

        public class CooperadoYAdministrador
        {
            [JsonPropertyName("rut")]
            public int Rut { get; set; }

            [JsonPropertyName("dv")]
            public string DV { get; set; }

            [JsonPropertyName("rutDV")]
            public string RutDV { get; set; }

            [JsonPropertyName("primerApellido")]
            public string PrimerApellido { get; set; }

            [JsonPropertyName("segundoApellido")]
            public string SegundoApellido { get; set; }

            [JsonPropertyName("nombres")]
            public string Nombres { get; set; }

            [JsonPropertyName("razonSocial")]
            public string RazonSocial { get; set; }

            [JsonPropertyName("calle")]
            public string Calle { get; set; }

            [JsonPropertyName("numero")]
            public string Numero { get; set; }

            [JsonPropertyName("bloque")]
            public string Bloque { get; set; }

            [JsonPropertyName("departamento")]
            public string Departamento { get; set; }

            [JsonPropertyName("villaPoblacion")]
            public string VillaPoblacion { get; set; }

            [JsonPropertyName("region")]
            public int Region { get; set; }

            [JsonPropertyName("comuna")]
            public int Comuna { get; set; }

            [JsonPropertyName("direccion")]
            public string Direccion { get; set; }

            [JsonPropertyName("telefono")]
            public string Telefono { get; set; }

            [JsonPropertyName("eMail")]
            public string EMail { get; set; }

            [JsonPropertyName("adjuntarDocumento")]
            public Documento AdjuntarDocumento { get; set; }

            [JsonPropertyName("representante")]
            public List<Representante> Representante { get; set; }

            [JsonPropertyName("calidad")]
            public List<Calidad> Calidad { get; set; }

            [JsonPropertyName("capitalDelSocio")]
            public List<CapitalDelSocio> CapitalDelSocio { get; set; }
        }

        public class DatosDelSistema
        {
            [JsonPropertyName("numeroDeAtencion")]
            public int NumeroDeAtencion { get; set; }

            [JsonPropertyName("numeroTotalSocios")]
            public int NumeroTotalSocios { get; set; }

            [JsonPropertyName("numeroSociosHombres")]
            public int NumeroSociosHombres { get; set; }

            [JsonPropertyName("numeroSociasMujeres")]
            public int NumeroSociasMujeres { get; set; }

            [JsonPropertyName("rutSolicitante")]
            public int RutSolicitante { get; set; }

            [JsonPropertyName("dv")]
            public string Dv { get; set; }

            [JsonPropertyName("rutDV")]
            public string RutDV { get; set; }

            [JsonPropertyName("nombresSolicitante")]
            public string NombresSolicitante { get; set; }

            [JsonPropertyName("primerApellido")]
            public string PrimerApellido { get; set; }

            [JsonPropertyName("segundoApellido")]
            public string SegundoApellido { get; set; }

            [JsonPropertyName("emailSolicitante")]
            public string EmailSolicitante { get; set; }

            [JsonPropertyName("fonoSolicitante")]
            public string FonoSolicitante { get; set; }

            [JsonPropertyName("fechaCelebracion")]
            public string FechaCelebracion { get; set; }

            internal bool NullParameter()
            {
                return false;
            }
        }

        public class DerechosYObligacionesSocios
        {
            [JsonPropertyName("aporteParaIngresoACooperativa")]
            public string AporteParaIngresoACooperativa { get; set; }
        }

        public class DireccionDeLaCooperativa
        {
            [JsonPropertyName("calle")]
            public string Calle { get; set; }

            [JsonPropertyName("numero")]
            public string Numero { get; set; }

            [JsonPropertyName("bloque")]
            public string Bloque { get; set; }

            [JsonPropertyName("departamento")]
            public string Departamento { get; set; }

            [JsonPropertyName("villaPoblacion")]
            public string VillaPoblacion { get; set; }

            [JsonPropertyName("region")]
            public int Region { get; set; }

            [JsonPropertyName("comuna")]
            public int Comuna { get; set; }

            [JsonPropertyName("direccion")]
            public string Direccion { get; set; }

            internal bool NullParameter()
            {
                return ((this.Direccion is null));
            }
        }

        public class Documentos
        {
            [JsonPropertyName("escrituraPublicaConstitucion")]
            public Documento EscrituraPublicaConstitucion { get; set; }

            [JsonPropertyName("inscripcionExtractoCBR")]
            public Documento InscripcionExtractoCBR { get; set; }

            [JsonPropertyName("publicacionDiarioOficial")]
            public Documento PublicacionDiarioOficial { get; set; }

            [JsonPropertyName("otrosDocumentos")]
            public List<Documento> OtrosDocumentos { get; set; }

            internal bool NullParameter()
            {
                return ((this.EscrituraPublicaConstitucion is null) ||
                        (this.InscripcionExtractoCBR is null) ||
                        (this.PublicacionDiarioOficial is null) ||
                        (this.OtrosDocumentos is null));
            }
        }

        public class DuracionDeLaCooperativa
        {
            [JsonPropertyName("indefinido")]
            public int Indefinido { get; set; }

            [JsonPropertyName("anhos")]
            public int Anhos { get; set; }
        }

        public class JuntaDeVigilancia
        {
            [JsonPropertyName("numeroMiembros")]
            public int NumeroMiembros { get; set; }

            [JsonPropertyName("plazoDuracion")]
            public int PlazoDuracion { get; set; }

            [JsonPropertyName("derechoReeleccion")]
            public int DerechoReeleccion { get; set; }
        }

        public class NombreCooperativa
        {
            [JsonPropertyName("razonSocial")]
            public string RazonSocial { get; set; }

            [JsonPropertyName("nombreFantasiaOSigla")]
            public string NombreFantasiaOSigla { get; set; }

            internal bool NullParameter()
            {
                return ((this.RazonSocial is null));
            }
        }

        public class ObjetoSocial
        {
            [JsonPropertyName("rubro")]
            public int Rubro { get; set; }

            [JsonPropertyName("subRubroEspecifico")]
            public int SubRubroEspecifico { get; set; }

            [JsonPropertyName("actividades")]
            public List<int> Actividades { get; set; }

            internal bool NullParameter()
            {
                return false;
            }
        }

        public class OtrosAcuerdos
        {
            [JsonPropertyName("inclusiva")]
            public int Inclusiva { get; set; }

            [JsonPropertyName("exclusivaMujeres")]
            public int ExclusivaMujeres { get; set; }

            [JsonPropertyName("calidadIndigena")]
            public int CalidadIndigena { get; set; }

            internal bool NullParameter()
            {
                return false;
            }
        }

        public class Representante
        {
            [JsonPropertyName("rut")]
            public int Rut { get; set; }

            [JsonPropertyName("dv")]
            public string Dv { get; set; }

            [JsonPropertyName("rutDV")]
            public string RutDV { get; set; }

            [JsonPropertyName("primerApellido")]
            public string PrimerApellido { get; set; }

            [JsonPropertyName("segundoApellido")]
            public string SegundoApellido { get; set; }

            [JsonPropertyName("nombres")]
            public string Nombres { get; set; }

            [JsonPropertyName("adjuntarPoderRepresentacion")]
            public Documento AdjuntarPoderRepresentacion { get; set; }
        }

        public class Documento
        {
            private string _fileName;

            [JsonPropertyName("url")]
            public string URL { get; set; }

            [JsonPropertyName("nombreArchivo")]
            public string NombreArchivo {
                get
                {
                    if (string.IsNullOrWhiteSpace(_fileName))
                    {
                        return "indefinido";
                    }
                    return _fileName;
                }
                set { _fileName = value; }
            }
        }

    }
}
