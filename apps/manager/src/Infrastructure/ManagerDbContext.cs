using Manager.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infrastructure;

public class ManagerDbContext : DbContext
{
    public ManagerDbContext(DbContextOptions<ManagerDbContext> options)
        : base(options) { }

    public DbSet<EmployeeDbModel> Employees { get; set; }
}
