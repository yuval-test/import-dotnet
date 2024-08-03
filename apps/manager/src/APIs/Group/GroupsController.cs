using Microsoft.AspNetCore.Mvc;

namespace Manager.APIs;

[ApiController()]
public class GroupsController : GroupsControllerBase
{
    public GroupsController(IGroupsService service)
        : base(service) { }
}
