using Microsoft.AspNetCore.Mvc;

namespace Manager.APIs;

[ApiController()]
public class EmployeesController : EmployeesControllerBase
{
    public EmployeesController(IEmployeesService service)
        : base(service) { }
}
