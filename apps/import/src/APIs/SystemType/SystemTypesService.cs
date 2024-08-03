using Import.Infrastructure;

namespace Import.APIs;

public class SystemTypesService : SystemTypesServiceBase
{
    public SystemTypesService(ImportDbContext context)
        : base(context) { }
}
