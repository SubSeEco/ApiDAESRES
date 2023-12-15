using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{

    [Table("Rubro")]
    public class Rubro
    {
        public Rubro()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RubroId { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
    }
}