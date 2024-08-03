using Import.Infrastructure;

namespace Import.APIs;

public class ContractsService : ContractsServiceBase
{
    public ContractsService(ImportDbContext context)
        : base(context) { }
}
