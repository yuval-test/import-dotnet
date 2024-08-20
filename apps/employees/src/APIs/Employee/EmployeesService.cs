using Employees.Infrastructure;

namespace Employees.APIs;

public class EmployeesService : EmployeesServiceBase
{
    public EmployeesService(EmployeesDbContext context)
        : base(context) { }
}
