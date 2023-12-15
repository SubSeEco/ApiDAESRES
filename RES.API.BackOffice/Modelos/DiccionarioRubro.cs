using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("DiccionarioRubro")]
    public class DiccionarioRubro
    {
        public DiccionarioRubro()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Código RES")]
        public int CodigoRES { get; set; }
        [Display(Name = "Codigo Id Rubro - DAES")]
        public int RubroId { get; set; }
    }
}