using Manager.APIs.Common;
using Manager.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class EmployeeFindManyArgs : FindManyInput<Employee, EmployeeWhereInput> { }
