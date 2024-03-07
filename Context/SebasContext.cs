using Microsoft.EntityFrameworkCore;
using WebApplicationsebas.Models;

namespace WebApplicationsebas.Context
{
    public class SebasContext:DbContext
    {
        public SebasContext(DbContextOptions<SebasContext>contextOptions): base(contextOptions)
        {
        }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Automovil> Automoviles {  get; set; }
    }
}
