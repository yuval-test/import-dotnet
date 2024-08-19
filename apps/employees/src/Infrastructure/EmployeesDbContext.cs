using Microsoft.EntityFrameworkCore;
using Employees.Infrastructure.Models;

namespace Employees.Infrastructure;

public class EmployeesDbContext : DbContext
{
    public EmployeesDbContext (DbContextOptions<EmployeesDbContext> options): base(options) {
    }

    public DbSet<EmployeeDbModel> Employees { get; set; }
}
