using Microsoft.AspNetCore.Mvc;
using Employees.APIs.Common;
using Employees.Infrastructure.Models;

namespace Employees.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class EmployeeFindManyArgs : FindManyInput<Employee, EmployeeWhereInput>
{
}
