using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationsebas.Models
{
    public class Automovil
    {
        [Table("Automoviles")]

        public class Automovil
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public string NombreConductor { get; set; }

            public int AñoExpiraLicencia { get; set; }

            public string Foto { get; set; }



        }
    }
}
