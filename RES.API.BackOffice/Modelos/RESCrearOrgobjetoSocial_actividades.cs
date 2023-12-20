using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("RESCrearOrgobjetoSocial_actividades")]
    public class RESCrearOrgobjetoSocial_actividades
    {
        public RESCrearOrgobjetoSocial_actividades()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RESCrearOrgobjetoSocial_actividadesId { get; set; }
        [Display(Name = "RESCrearOrgMensajeId")]
        public int RESCrearOrgMensajeId { get; set; }
        [Display(Name = "actividad")]
        public int actividad { get; set; }
    }
}