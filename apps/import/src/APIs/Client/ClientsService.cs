using Import.Infrastructure;

namespace Import.APIs;

public class ClientsService : ClientsServiceBase
{
    public ClientsService(ImportDbContext context)
        : base(context) { }
}
