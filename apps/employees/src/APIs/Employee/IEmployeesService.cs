using Employees.APIs.Common;
using Employees.APIs.Dtos;

namespace Employees.APIs;

public interface IEmployeesService
{
    /// <summary>
    /// Create one Employee
    /// </summary>
    public Task<Employee> CreateEmployee(EmployeeCreateInput employee);

    /// <summary>
    /// Delete one Employee
    /// </summary>
    public Task DeleteEmployee(EmployeeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Employees
    /// </summary>
    public Task<List<Employee>> Employees(EmployeeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    public Task<MetadataDto> EmployeesMeta(EmployeeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Employee
    /// </summary>
    public Task<Employee> Employee(EmployeeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Employee
    /// </summary>
    public Task UpdateEmployee(EmployeeWhereUniqueInput uniqueId, EmployeeUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Employees records to Employee
    /// </summary>
    public Task ConnectEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );

    /// <summary>
    /// Disconnect multiple Employees records from Employee
    /// </summary>
    public Task DisconnectEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );

    /// <summary>
    /// Find multiple Employees records for Employee
    /// </summary>
    public Task<List<Employee>> FindEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeFindManyArgs EmployeeFindManyArgs
    );

    /// <summary>
    /// Update multiple Employees records for Employee
    /// </summary>
    public Task UpdateEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );

    /// <summary>
    /// Get a Manager record for Employee
    /// </summary>
    public Task<Employee> GetManager(EmployeeWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Supervisees records to Employee
    /// </summary>
    public Task ConnectSupervisees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );

    /// <summary>
    /// Disconnect multiple Supervisees records from Employee
    /// </summary>
    public Task DisconnectSupervisees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );

    /// <summary>
    /// Find multiple Supervisees records for Employee
    /// </summary>
    public Task<List<Employee>> FindSupervisees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeFindManyArgs EmployeeFindManyArgs
    );

    /// <summary>
    /// Update multiple Supervisees records for Employee
    /// </summary>
    public Task UpdateSupervisees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );

    /// <summary>
    /// Get a Supervisor record for Employee
    /// </summary>
    public Task<Employee> GetSupervisor(EmployeeWhereUniqueInput uniqueId);
}
