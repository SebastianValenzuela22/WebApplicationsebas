using Microsoft.EntityFrameworkCore;
using WebApplicationsebas.Models;

namespace WebApplicationsebas.Context
{
    public class SebasContext:DbContext
    {
        public SebasContext(DbContextOptions<SebasContext>contextOptions): base(contextOptions)
        {
        }
        public DbSet<Conductor> Conductor { get; set; }
        public DbSet<Automovil> Automovil {  get; set; }
    }
}
