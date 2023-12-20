using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("RESCrearOrgMensaje")]
    public class RESCrearOrgMensaje
    {
        public RESCrearOrgMensaje()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RESCrearOrgMensajeId { get; set; }

        [Display(Name = "Proceso Id")]
        public int ProcesoId { get; set; }

        [Display(Name = "Rubro Coooperativa")]
        public int objetoSocial_rubro { get; set; }

        [Display(Name = "SubRubro Especifico")]
        public int objetoSocial_subRubroEspecifico { get; set; }
        [Display(Name = "Razon Social")]
        public string nombreCooperativa_razonSocial { get; set; }
        [Display(Name = "Nombre Fantasia o Sigla")]
        public string nombreCooperativa_nombreFantasiaOSigla { get; set; }
        [Display(Name = "Calle")]
        public string direccionDeLaCooperativa_calle { get; set; }
        [Display(Name = "Numero")]
        public string direccionDeLaCooperativa_numero { get; set; }
        [Display(Name = "Bloque")]
        public string direccionDeLaCooperativa_bloque { get; set; }
        [Display(Name = "Departamento")]
        public string direccionDeLaCooperativa_departamento { get; set; }
        [Display(Name = "Villa o Poblacion")]
        public string direccionDeLaCooperativa_villaPoblacion { get; set; }
        [Display(Name = "Region")]
        public int direccionDeLaCooperativa_region { get; set; }
        [Display(Name = "Comuna")]
        public int direccionDeLaCooperativa_comuna { get; set; }
        [Display(Name = "Direccion")]
        public string direccionDeLaCooperativa_direccion { get; set; }
        [Display(Name = "EMail")]
        public string contactoDeLaCooperativa_eMail { get; set; }
        [Display(Name = "Telefono")]
        public string contactoDeLaCooperativa_telefono { get; set; }
        [Display(Name = "Pagina Web")]
        public string contactoDeLaCooperativa_paginaWeb { get; set; }
        [Display(Name = "Monto Total de Capital Inicial")]
        public int capitalDeLaCooperativa_montoTotalCapitalInicial { get; set; }
        [Display(Name = "Numero de Cuotas Participacion")]
        public int capitalDeLaCooperativa_numeroCuotasParticipacion { get; set; }
        [Display(Name = "Valor de Cuotas Paticipacion")]
        public int capitalDeLaCooperativa_valorCuotasPaticipacion { get; set; }
        [Display(Name = "Indefinido")]
        public int duracionDeLaCooperativa_indefinido { get; set; }
        [Display(Name = "Años")]
        public int duracionDeLaCooperativa_anhos { get; set; }
        [Display(Name = "Tipo Administracion")]
        public int administracionDeLaCooperativa_tipoAdministracion { get; set; }
        [Display(Name = "Numero Miembros")]
        public int consejoAdministracion_numeroMiembros { get; set; }
        [Display(Name = "Plazo Duracion")]
        public int consejoAdministracion_plazoDuracion { get; set; }
        [Display(Name = "Derecho Reeleccion")]
        public int consejoAdministracion_derechoReeleccion { get; set; }
        [Display(Name = "Numero Miembros")]
        public int juntaDeVigilancia_numeroMiembros { get; set; }
        [Display(Name = "Plazo Duracion")]
        public int juntaDeVigilancia_plazoDuracion { get; set; }
        [Display(Name = "Derecho Reeleccion")]
        public int juntaDeVigilancia_derechoReeleccion { get; set; }
        [Display(Name = "Aporte Para Ingreso a Cooperativa")]
        public string derechosYObligacionesSocios_aporteParaIngresoACooperativa { get; set; }
        [Display(Name = "Inclusiva")]
        public int otrosAcuerdos_inclusiva { get; set; }
        [Display(Name = "Exclusiva Mujeres")]
        public int otrosAcuerdos_exclusivaMujeres { get; set; }
        [Display(Name = "Calidad Indigena")]
        public int otrosAcuerdos_calidadIndigena { get; set; }
        [Display(Name = "rul de EscrituraPublicaConstitucion")]
        public string documentos_escrituraPublicaConstitucion_url { get; set; }
        [Display(Name = "nombreArchivo del EscrituraPublicaConstitucion")]
        public string documentos_escrituraPublicaConstitucion_nombreArchivo { get; set; }
        [Display(Name = "url de Inscripcion Extracto CBR")]
        public string documentos_inscripcionExtractoCBR_url { get; set; }
        [Display(Name = "nombreArchivo del Inscripcion Extracto CBR")]
        public string documentos_inscripcionExtractoCBR_nombreArchivo { get; set; }
        [Display(Name = "url de Publicacion Diario Oficial")]
        public string documentos_publicacionDiarioOficial_url { get; set; }
        [Display(Name = "nombreArchivo del Publicacion Diario Oficial")]
        public string documentos_publicacionDiarioOficial_nombreArchivo { get; set; }
        [Display(Name = "Numero De Atencion")]
        public int datosDelSistema_numeroDeAtencion { get; set; }
        [Display(Name = "Numero Total de Socios")]
        public int datosDelSistema_numeroTotalSocios { get; set; }
        [Display(Name = "Numero de Socios Hombres")]
        public int datosDelSistema_numeroSociosHombres { get; set; }
        [Display(Name = "Numero de Socias Mujeres")]
        public int datosDelSistema_numeroSociasMujeres { get; set; }
        [Display(Name = "Rut Solicitante")]
        public int datosDelSistema_rutSolicitante { get; set; }
        [Display(Name = "dv")]
        public string datosDelSistema_dv { get; set; }
        [Display(Name = "rutDV")]
        public string datosDelSistema_rutDV { get; set; }
        [Display(Name = "Nombres Solicitante")]
        public string datosDelSistema_nombresSolicitante { get; set; }
        [Display(Name = "Primer Apellido")]
        public string datosDelSistema_primerApellido { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string datosDelSistema_segundoApellido { get; set; }
        [Display(Name = "EMail Solicitante")]
        public string datosDelSistema_emailSolicitante { get; set; }
        [Display(Name = "Fono Solicitante")]
        public string datosDelSistema_fonoSolicitante { get; set; }
        [Display(Name = "Fecha de Celebracion")]
        public string datosDelSistema_fechaCelebracion { get; set; }
    }
}