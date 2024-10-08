using Manager.APIs;
using Manager.APIs.Common;
using Manager.APIs.Dtos;
using Manager.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Manager.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EmployeesControllerBase : ControllerBase
{
    protected readonly IEmployeesService _service;

    public EmployeesControllerBase(IEmployeesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Employee
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Employee>> CreateEmployee(EmployeeCreateInput input)
    {
        var employee = await _service.CreateEmployee(input);

        return CreatedAtAction(nameof(Employee), new { id = employee.Id }, employee);
    }

    /// <summary>
    /// Delete one Employee
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEmployee([FromRoute()] EmployeeWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteEmployee(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Employees
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Employee>>> Employees(
        [FromQuery()] EmployeeFindManyArgs filter
    )
    {
        return Ok(await _service.Employees(filter));
    }

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EmployeesMeta(
        [FromQuery()] EmployeeFindManyArgs filter
    )
    {
        return Ok(await _service.EmployeesMeta(filter));
    }

    /// <summary>
    /// Get one Employee
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Employee>> Employee(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Employee(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Employee
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEmployee(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromQuery()] EmployeeUpdateInput employeeUpdateDto
    )
    {
        try
        {
            await _service.UpdateEmployee(uniqueId, employeeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Employees records to Employee
    /// </summary>
    [HttpPost("{Id}/employees")]
    public async Task<ActionResult> ConnectEmployees(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromQuery()] EmployeeWhereUniqueInput[] employeesId
    )
    {
        try
        {
            await _service.ConnectEmployees(uniqueId, employeesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Employees records from Employee
    /// </summary>
    [HttpDelete("{Id}/employees")]
    public async Task<ActionResult> DisconnectEmployees(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromBody()] EmployeeWhereUniqueInput[] employeesId
    )
    {
        try
        {
            await _service.DisconnectEmployees(uniqueId, employeesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Employees records for Employee
    /// </summary>
    [HttpGet("{Id}/employees")]
    public async Task<ActionResult<List<Employee>>> FindEmployees(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromQuery()] EmployeeFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindEmployees(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Employees records for Employee
    /// </summary>
    [HttpPatch("{Id}/employees")]
    public async Task<ActionResult> UpdateEmployees(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromBody()] EmployeeWhereUniqueInput[] employeesId
    )
    {
        try
        {
            await _service.UpdateEmployees(uniqueId, employeesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple groups records to Employee
    /// </summary>
    [HttpPost("{Id}/groups")]
    public async Task<ActionResult> ConnectGroups(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromQuery()] GroupWhereUniqueInput[] groupsId
    )
    {
        try
        {
            await _service.ConnectGroups(uniqueId, groupsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple groups records from Employee
    /// </summary>
    [HttpDelete("{Id}/groups")]
    public async Task<ActionResult> DisconnectGroups(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromBody()] GroupWhereUniqueInput[] groupsId
    )
    {
        try
        {
            await _service.DisconnectGroups(uniqueId, groupsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple groups records for Employee
    /// </summary>
    [HttpGet("{Id}/groups")]
    public async Task<ActionResult<List<Group>>> FindGroups(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromQuery()] GroupFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindGroups(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple groups records for Employee
    /// </summary>
    [HttpPatch("{Id}/groups")]
    public async Task<ActionResult> UpdateGroups(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId,
        [FromBody()] GroupWhereUniqueInput[] groupsId
    )
    {
        try
        {
            await _service.UpdateGroups(uniqueId, groupsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Manager record for Employee
    /// </summary>
    [HttpGet("{Id}/employees")]
    public async Task<ActionResult<List<Employee>>> GetManager(
        [FromRoute()] EmployeeWhereUniqueInput uniqueId
    )
    {
        var employee = await _service.GetManager(uniqueId);
        return Ok(employee);
    }
}
