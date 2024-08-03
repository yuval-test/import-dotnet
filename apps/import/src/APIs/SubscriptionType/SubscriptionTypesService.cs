using Import.Infrastructure;

namespace Import.APIs;

public class SubscriptionTypesService : SubscriptionTypesServiceBase
{
    public SubscriptionTypesService(ImportDbContext context)
        : base(context) { }
}
