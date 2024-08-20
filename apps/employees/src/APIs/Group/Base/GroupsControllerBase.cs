using Employees.APIs;
using Employees.APIs.Common;
using Employees.APIs.Dtos;
using Employees.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Employees.APIs;

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
}
