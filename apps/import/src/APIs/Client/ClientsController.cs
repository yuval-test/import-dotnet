using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[ApiController()]
public class ClientsController : ClientsControllerBase
{
    public ClientsController(IClientsService service)
        : base(service) { }
}
