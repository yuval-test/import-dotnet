using Manager.Infrastructure;

namespace Manager.APIs;

public class GroupsService : GroupsServiceBase
{
    public GroupsService(ManagerDbContext context)
        : base(context) { }
}
