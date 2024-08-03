using Manager.APIs;
using Manager.APIs.Common;
using Manager.APIs.Dtos;
using Manager.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Manager.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class GroupsControllerBase : ControllerBase
{
    protected readonly IGroupsService _service;

    public GroupsControllerBase(IGroupsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one group
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Group>> CreateGroup(GroupCreateInput input)
    {
        var group = await _service.CreateGroup(input);

        return CreatedAtAction(nameof(Group), new { id = group.Id }, group);
    }

    /// <summary>
    /// Delete one group
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteGroup([FromRoute()] GroupWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteGroup(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many groups
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Group>>> Groups([FromQuery()] GroupFindManyArgs filter)
    {
        return Ok(await _service.Groups(filter));
    }

    /// <summary>
    /// Meta data about group records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> GroupsMeta([FromQuery()] GroupFindManyArgs filter)
    {
        return Ok(await _service.GroupsMeta(filter));
    }

    /// <summary>
    /// Get one group
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Group>> Group([FromRoute()] GroupWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Group(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one group
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateGroup(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
        [FromQuery()] GroupUpdateInput groupUpdateDto
    )
    {
        try
        {
            await _service.UpdateGroup(uniqueId, groupUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple employees records to group
    /// </summary>
    [HttpPost("{Id}/employees")]
    public async Task<ActionResult> ConnectEmployees(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
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
    /// Disconnect multiple employees records from group
    /// </summary>
    [HttpDelete("{Id}/employees")]
    public async Task<ActionResult> DisconnectEmployees(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
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
    /// Find multiple employees records for group
    /// </summary>
    [HttpGet("{Id}/employees")]
    public async Task<ActionResult<List<Employee>>> FindEmployees(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
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
    /// Update multiple employees records for group
    /// </summary>
    [HttpPatch("{Id}/employees")]
    public async Task<ActionResult> UpdateEmployees(
        [FromRoute()] GroupWhereUniqueInput uniqueId,
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
}
