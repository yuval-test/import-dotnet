using Microsoft.AspNetCore.Mvc;

namespace Employees.APIs;

[ApiController()]
public class GroupsController : GroupsControllerBase
{
    public GroupsController(IGroupsService service)
        : base(service) { }
}
