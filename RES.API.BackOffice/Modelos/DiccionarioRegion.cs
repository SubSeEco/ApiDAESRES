using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("DiccionarioRegion")]
    public class DiccionarioRegion
    {
        public DiccionarioRegion()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Código RES")]
        public int CodigoRES { get; set; }
        [Display(Name = "Codigo Id Region - DAES")]
        public int RegionId { get; set; }
    }
}