using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[ApiController()]
public class SubscriptionTypesController : SubscriptionTypesControllerBase
{
    public SubscriptionTypesController(ISubscriptionTypesService service)
        : base(service) { }
}
