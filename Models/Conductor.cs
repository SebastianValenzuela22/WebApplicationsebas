using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplicationsebas.Models
{
    [Table("Conductores")]

    public class Conductor
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NombreConductor { get; set; }

        public int AñoExpiraLicencia { get; set; }


        

    }
}
