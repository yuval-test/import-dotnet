using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
