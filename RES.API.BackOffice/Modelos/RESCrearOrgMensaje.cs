﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAES.API.BackOffice.Modelos
{
    
    [Table("RESCrearOrgMensaje")]
    public class RESCrearOrgMensaje
    {
        public RESCrearOrgMensaje()
        {
               
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Id")]
        public int RESCrearOrgMensajeId { get; set; }

        [Display(Name = "Proceso Id")]
        public int ProcesoId { get; set; }

        [Display(Name = "Rubro Coooperativa")]
        public int objetoSocial_rubro { get; set; }
    }
}