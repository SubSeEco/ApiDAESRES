using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("RESCrearOrgCooperadosYAdministradores")]
    public class RESCrearOrgCooperadosYAdministradores
    {
        public RESCrearOrgCooperadosYAdministradores()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RESCrearOrgCooperadosYAdministradoresId { get; set; }
        [Display(Name = "RESCrearOrgMensajeId")]
        public int RESCrearOrgMensajeId { get; set; }
        [Display(Name = "rut")]
        public int rut { get; set; }
        [Display(Name = "dv")]
        public string dv { get; set; }
        [Display(Name = "rutDV")]
        public string rutDV { get; set; }
        [Display(Name = "primerApellido")]
        public string primerApellido { get; set; }
        [Display(Name = "segundoApellido")]
        public string segundoApellido { get; set; }
        [Display(Name = "nombres")]
        public string nombres { get; set; }
        [Display(Name = "razonSocial")]
        public string razonSocial { get; set; }
        [Display(Name = "calle")]
        public string calle { get; set; }
        [Display(Name = "numero")]
        public string numero { get; set; }
        [Display(Name = "bloque")]
        public string bloque { get; set; }
        [Display(Name = "departamento")]
        public string departamento { get; set; }
        [Display(Name = "villaPoblacion")]
        public string villaPoblacion { get; set; }
        [Display(Name = "region")]
        public int region { get; set; }
        [Display(Name = "comuna")]
        public int comuna { get; set; }
        [Display(Name = "direccion")]
        public string direccion { get; set; }
        [Display(Name = "telefono")]
        public string telefono { get; set; }
        [Display(Name = "eMail")]
        public string eMail { get; set; }
        [Display(Name = "url del adjuntarDocumento")]
        public string adjuntarDocumento_url { get; set; }
        [Display(Name = "nombreArchivo del adjuntarDocumento")]
        public string adjuntarDocumento_nombreArchivo { get; set; }

    }
}