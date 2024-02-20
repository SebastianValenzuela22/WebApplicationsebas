using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationsebas.Models
{
    [Table("Automoviles")]
    public class Automovil
    {
        
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public string Modelo { get; set; }

            public int Año { get; set; }

            public string Foto { get; set; }

            public virtual Conductor conductor {  get; set; } //representa la llave foranea

        } 
    }

