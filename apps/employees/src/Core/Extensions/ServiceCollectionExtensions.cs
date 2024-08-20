using Employees.APIs;

namespace Employees;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<IGroupsService, GroupsService>();
    }
}
