using Manager.Infrastructure;

namespace Manager.APIs;

public class EmployeesService : EmployeesServiceBase
{
    public EmployeesService(ManagerDbContext context)
        : base(context) { }
}
