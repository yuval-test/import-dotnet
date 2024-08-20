using Employees.APIs.Dtos;
using Employees.Infrastructure.Models;

namespace Employees.APIs.Extensions;

public static class EmployeesExtensions
{
    public static Employee ToDto(this EmployeeDbModel model)
    {
        return new Employee
        {
            CreatedAt = model.CreatedAt,
            Employees = model.Employees?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Manager = model.ManagerId,
            Name = model.Name,
            Phone = model.Phone,
            StartDate = model.StartDate,
            Supervisees = model.Supervisees?.Select(x => x.Id).ToList(),
            Supervisor = model.SupervisorId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EmployeeDbModel ToModel(
        this EmployeeUpdateInput updateDto,
        EmployeeWhereUniqueInput uniqueId
    )
    {
        var employee = new EmployeeDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Phone = updateDto.Phone,
            StartDate = updateDto.StartDate
        };

        if (updateDto.CreatedAt != null)
        {
            employee.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Manager != null)
        {
            employee.ManagerId = updateDto.Manager;
        }
        if (updateDto.Supervisor != null)
        {
            employee.SupervisorId = updateDto.Supervisor;
        }
        if (updateDto.UpdatedAt != null)
        {
            employee.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return employee;
    }
}
