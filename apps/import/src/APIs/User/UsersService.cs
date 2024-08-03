using Import.Infrastructure;

namespace Import.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(ImportDbContext context)
        : base(context) { }
}
