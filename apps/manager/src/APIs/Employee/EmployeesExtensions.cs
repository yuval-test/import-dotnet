using Manager.APIs.Dtos;
using Manager.Infrastructure.Models;

namespace Manager.APIs.Extensions;

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
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EmployeeDbModel ToModel(
        this EmployeeUpdateInput updateDto,
        EmployeeWhereUniqueInput uniqueId
    )
    {
        var employee = new EmployeeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            employee.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Manager != null)
        {
            employee.ManagerId = updateDto.Manager;
        }
        if (updateDto.UpdatedAt != null)
        {
            employee.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return employee;
    }
}
