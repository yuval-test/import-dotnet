using Microsoft.AspNetCore.Mvc;
using Employees.APIs;
using Employees.APIs.Dtos;
using Employees.APIs.Errors;
using Employees.APIs.Common;

namespace Employees.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EmployeesControllerBase : ControllerBase
{
    protected readonly IEmployeesService _service;
    public EmployeesControllerBase (IEmployeesService service) {
        _service = service;}

    /// <summary>
    /// Create one Employee
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Employee>> CreateEmployee(EmployeeCreateInput input) {
        var employee = await _service.CreateEmployee(input);
        
    return CreatedAtAction(nameof(Employee), new { id = employee.Id }, employee);}

    /// <summary>
    /// Delete one Employee
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEmployee([FromRoute()]
    EmployeeWhereUniqueInput uniqueId) {
        try
            {
        await _service.DeleteEmployee(uniqueId);
    }
    catch (NotFoundException)
    {
        return NotFound();
    }

    return NoContent();}

    /// <summary>
    /// Find many Employees
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Employee>>> Employees([FromQuery()]
    EmployeeFindManyArgs filter) {
        return Ok(await _service.Employees(filter));}

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EmployeesMeta([FromQuery()]
    EmployeeFindManyArgs filter) {
        return Ok(await _service.EmployeesMeta(filter));}

    /// <summary>
    /// Get one Employee
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Employee>> Employee([FromRoute()]
    EmployeeWhereUniqueInput uniqueId) {
         try
            {
        return await _service.Employee(uniqueId);
    }
    catch (NotFoundException)
    {
        return NotFound();
    }}

    /// <summary>
    /// Update one Employee
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEmployee([FromRoute()]
    EmployeeWhereUniqueInput uniqueId, [FromQuery()]
    EmployeeUpdateInput employeeUpdateDto) {
        try
            {
        await _service.UpdateEmployee(uniqueId, employeeUpdateDto);
    }
    catch (NotFoundException)
    {
        return NotFound();
    }

    return NoContent();}

    /// <summary>
    /// Connect multiple Supervisees records to Employee
    /// </summary>
    [HttpPost("{Id}/employees")]
    public async Task<ActionResult> ConnectSupervisees([FromRoute()]
    EmployeeWhereUniqueInput uniqueId, [FromQuery()]
    EmployeeWhereUniqueInput[] employeesId) {
        try
            {
        await _service.ConnectSupervisees(uniqueId, employeesId);
    }
    catch (NotFoundException)
    {
        return NotFound();
    }

    return NoContent();}

    /// <summary>
    /// Disconnect multiple Supervisees records from Employee
    /// </summary>
    [HttpDelete("{Id}/employees")]
    public async Task<ActionResult> DisconnectSupervisees([FromRoute()]
    EmployeeWhereUniqueInput uniqueId, [FromBody()]
    EmployeeWhereUniqueInput[] employeesId) {
        try
            {
        await _service.DisconnectSupervisees(uniqueId, employeesId);
    }
    catch (NotFoundException)
    {
        return NotFound();
    }

    return NoContent();}

    /// <summary>
    /// Find multiple Supervisees records for Employee
    /// </summary>
    [HttpGet("{Id}/employees")]
    public async Task<ActionResult<List<Employee>>> FindSupervisees([FromRoute()]
    EmployeeWhereUniqueInput uniqueId, [FromQuery()]
    EmployeeFindManyArgs filter) {
        try
            {
        return Ok(await _service.FindSupervisees(uniqueId, filter));
    }
    catch (NotFoundException)
    {
        return NotFound();
    }}

    /// <summary>
    /// Update multiple Supervisees records for Employee
    /// </summary>
    [HttpPatch("{Id}/employees")]
    public async Task<ActionResult> UpdateSupervisees([FromRoute()]
    EmployeeWhereUniqueInput uniqueId, [FromBody()]
    EmployeeWhereUniqueInput[] employeesId) {
        try
            {
        await _service.UpdateSupervisees(uniqueId, employeesId);
    }
    catch (NotFoundException)
    {
        return NotFound();
    }

    return NoContent();}

    /// <summary>
    /// Get a Supervisor record for Employee
    /// </summary>
    [HttpGet("{Id}/employees")]
    public async Task<ActionResult<List<Employee>>> GetSupervisor([FromRoute()]
    EmployeeWhereUniqueInput uniqueId) {
        var employee = await _service.GetSupervisor(uniqueId);
            return Ok(employee);}

}
