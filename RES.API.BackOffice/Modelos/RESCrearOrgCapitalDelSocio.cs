using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("RESCrearOrgCapitalDelSocio")]
    public class RESCrearOrgCapitalDelSocio
    {
        public RESCrearOrgCapitalDelSocio()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RESCrearOrgCapitalDelSocioId { get; set; }
        [Display(Name = "RESCrearOrgCooperadosYAdministradoresId")]
        public int RESCrearOrgCooperadosYAdministradoresId { get; set; }
        [Display(Name = "Tipos de Aporte")]
        public int tiposAporte { get; set; }
        [Display(Name = "Cantidad de Cuotas")]
        public int cantidadCuotas { get; set; }
        [Display(Name = "Capital de Pagado")]
        public int capitalPagado { get; set; }
        [Display(Name = "Capital por Pagar")]
        public int capitalPorPagar { get; set; }
        [Display(Name = "Plazo Para Pagar")]
        public int plazoParaPagar { get; set; }
        [Display(Name = "Forma En Que Sera Enterado")]
        public string formaEnQueSeraEnterado { get; set; }
        [Display(Name = "Descripcion Aporte")]
        public string descripcionAporte { get; set; }
    }
}