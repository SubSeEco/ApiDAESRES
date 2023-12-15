using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{

    [Table("SubRubro")]
    public class SubRubro
    {
        public SubRubro()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int SubRubroId { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Rubro")]
        public int RubroId { get; set; }
    }
}