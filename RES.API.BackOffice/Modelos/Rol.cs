    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace DAES.API.BackOffice.Modelos
    {
    
        [Table("Rol")]
        public class Rol
        {
            public Rol()
            {
               
            }

            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Display(Name = "Id")]
            public int RolId { get; set; }

        }
    }

