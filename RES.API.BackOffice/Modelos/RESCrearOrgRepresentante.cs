using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("RESCrearOrgRepresentante")]
    public class RESCrearOrgRepresentante
    {
        public RESCrearOrgRepresentante()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RESCrearOrgRepresentanteId { get; set; }
        [Display(Name = "RESCrearOrgCooperadosYAdministradoresId")]
        public int RESCrearOrgCooperadosYAdministradoresId { get; set; }
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
        [Display(Name = "url de Poder Representacion")]
        public string adjuntarPoderRepresentacion_url { get; set; }
        [Display(Name = "ombreArchivo de Poder Representacion")]
        public string adjuntarPoderRepresentacion_nombreArchivo { get; set; }
    }
}