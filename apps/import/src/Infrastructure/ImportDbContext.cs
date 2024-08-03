using Import.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Import.Infrastructure;

public class ImportDbContext : DbContext
{
    public ImportDbContext(DbContextOptions<ImportDbContext> options)
        : base(options) { }

    public DbSet<ClientDbModel> Clients { get; set; }

    public DbSet<ContractDbModel> Contracts { get; set; }

    public DbSet<SubscriptionTypeDbModel> SubscriptionTypes { get; set; }

    public DbSet<SystemTypeDbModel> SystemTypes { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
