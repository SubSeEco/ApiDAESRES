using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("DiccionarioComuna")]
    public class DiccionarioComuna
    {
        public DiccionarioComuna()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Código RES")]
        public int CodigoRES { get; set; }
        [Display(Name = "Codigo Id Comuna - DAES")]
        public int ComunaId { get; set; }
    }
}