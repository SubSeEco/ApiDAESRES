using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("DiccionarioSubRubro")]
    public class DiccionarioSubRubro
    {
        public DiccionarioSubRubro()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Código RES")]
        public int CodigoRES { get; set; }
        [Display(Name = "Codigo Id SubRubro - DAES")]
        public int SubRubroId { get; set; }
    }
}