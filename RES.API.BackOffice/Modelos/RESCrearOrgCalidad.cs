using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{

    [Table("RESCrearOrgCalidad")]
    public class RESCrearOrgCalidad
    {
        public RESCrearOrgCalidad()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RESCrearOrgCalidadId { get; set; }
        [Display(Name = "RESCrearOrgCooperadosYAdministradoresId")]
        public int RESCrearOrgCooperadosYAdministradoresId { get; set; }
        [Display(Name = "Calidad")]
        public int Calidad { get; set; }

    }
    }