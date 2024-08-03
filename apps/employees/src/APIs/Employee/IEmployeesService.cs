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
    /// Get a Employees record for Employee
    /// </summary>
    public Task<Employee> GetEmployees(EmployeeWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Supervisees record for Employee
    /// </summary>
    public Task<Employee> GetSupervisees(EmployeeWhereUniqueInput uniqueId);
}
