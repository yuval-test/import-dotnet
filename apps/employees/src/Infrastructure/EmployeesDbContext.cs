using Employees.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure;

public class EmployeesDbContext : DbContext
{
    public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options)
        : base(options) { }

    public DbSet<EmployeeDbModel> Employees { get; set; }
}
