using Employees.Infrastructure;

namespace Employees.APIs;

public class GroupsService : GroupsServiceBase
{
    public GroupsService(EmployeesDbContext context)
        : base(context) { }
}
