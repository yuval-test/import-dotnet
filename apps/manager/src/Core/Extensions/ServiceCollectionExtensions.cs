using Manager.APIs;

namespace Manager;

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
