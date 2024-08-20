using Microsoft.AspNetCore.Mvc;

namespace Employees.APIs;

[ApiController()]
public class EmployeesController : EmployeesControllerBase
{
    public EmployeesController(IEmployeesService service) : base(service)
    {
    }

}
