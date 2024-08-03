using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[ApiController()]
public class SystemTypesController : SystemTypesControllerBase
{
    public SystemTypesController(ISystemTypesService service)
        : base(service) { }
}
