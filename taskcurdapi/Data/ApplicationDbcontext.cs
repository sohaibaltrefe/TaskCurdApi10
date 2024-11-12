using Microsoft.EntityFrameworkCore;
using taskcurdapi.Models;

namespace taskcurdapi.Data
{
    public class ApplicationDbcontext :DbContext

    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext>options):base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department>  departments { get; set; }

    }
}
