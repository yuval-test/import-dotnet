using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[ApiController()]
public class ContractsController : ContractsControllerBase
{
    public ContractsController(IContractsService service)
        : base(service) { }
}
