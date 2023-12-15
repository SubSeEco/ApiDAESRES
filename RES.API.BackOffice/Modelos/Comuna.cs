using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("Comuna")]
    public class Comuna
    {
        public Comuna()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int ComunaId { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre {  get; set; }
        [Display(Name = "Ciudad")]
        public int CiudadId { get; set; }
        [Display(Name = "Región")]
        public int RegionId { get; set; }
        [Display(Name = "Código")]
        public int Codigo { get; set; }
    }
}