using Employees.APIs.Common;
using Employees.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class EmployeeFindManyArgs : FindManyInput<Employee, EmployeeWhereInput> { }
