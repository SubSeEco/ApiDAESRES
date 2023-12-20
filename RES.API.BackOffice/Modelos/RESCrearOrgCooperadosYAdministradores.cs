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

        [Display(Name = "Proceso Id")]
        public int ProcesoId { get; set; }

        [Display(Name = "Rubro Coooperativa")]
        public int objetoSocial_rubro { get; set; }
    }
}