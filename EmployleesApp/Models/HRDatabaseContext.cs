using Microsoft.EntityFrameworkCore;

namespace EmployleesApp.Models
{
    public class HRDatabaseContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-NEHU9IV; Initial Catalog=EnployeesDb; Integrated Security=SSPI;");
        }
    }
}
