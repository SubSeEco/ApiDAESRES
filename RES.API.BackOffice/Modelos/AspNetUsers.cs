using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RES.API.BackOffice.Modelos
{
    [Table("AspNetUsers")]
    public class AspNetUsers
    {
        public AspNetUsers()
        {
        }

        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
       
        public int PerfilId { get; set;  } 
        public bool? TareaAsignadaApi { get; set; }
    }
}
