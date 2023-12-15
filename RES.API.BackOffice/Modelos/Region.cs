using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{

    [Table("Region")]
    public class Region
    {
        public Region()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RegionId { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Código")]
        public int Codigo { get; set; }
    }
}