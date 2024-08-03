using Import.APIs;
using Import.APIs.Common;
using Import.APIs.Dtos;
using Import.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SystemTypesControllerBase : ControllerBase
{
    protected readonly ISystemTypesService _service;

    public SystemTypesControllerBase(ISystemTypesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one System Type
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<SystemType>> CreateSystemType(SystemTypeCreateInput input)
    {
        var systemType = await _service.CreateSystemType(input);

        return CreatedAtAction(nameof(SystemType), new { id = systemType.Id }, systemType);
    }

    /// <summary>
    /// Delete one System Type
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSystemType(
        [FromRoute()] SystemTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteSystemType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many SystemTypes
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<SystemType>>> SystemTypes(
        [FromQuery()] SystemTypeFindManyArgs filter
    )
    {
        return Ok(await _service.SystemTypes(filter));
    }

    /// <summary>
    /// Meta data about System Type records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SystemTypesMeta(
        [FromQuery()] SystemTypeFindManyArgs filter
    )
    {
        return Ok(await _service.SystemTypesMeta(filter));
    }

    /// <summary>
    /// Get one System Type
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<SystemType>> SystemType(
        [FromRoute()] SystemTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.SystemType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one System Type
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSystemType(
        [FromRoute()] SystemTypeWhereUniqueInput uniqueId,
        [FromQuery()] SystemTypeUpdateInput systemTypeUpdateDto
    )
    {
        try
        {
            await _service.UpdateSystemType(uniqueId, systemTypeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Client records to System Type
    /// </summary>
    [HttpPost("{Id}/clients")]
    public async Task<ActionResult> ConnectClient(
        [FromRoute()] SystemTypeWhereUniqueInput uniqueId,
        [FromQuery()] ClientWhereUniqueInput[] clientsId
    )
    {
        try
        {
            await _service.ConnectClient(uniqueId, clientsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Client records from System Type
    /// </summary>
    [HttpDelete("{Id}/clients")]
    public async Task<ActionResult> DisconnectClient(
        [FromRoute()] SystemTypeWhereUniqueInput uniqueId,
        [FromBody()] ClientWhereUniqueInput[] clientsId
    )
    {
        try
        {
            await _service.DisconnectClient(uniqueId, clientsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Client records for System Type
    /// </summary>
    [HttpGet("{Id}/clients")]
    public async Task<ActionResult<List<Client>>> FindClient(
        [FromRoute()] SystemTypeWhereUniqueInput uniqueId,
        [FromQuery()] ClientFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindClient(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Client records for System Type
    /// </summary>
    [HttpPatch("{Id}/clients")]
    public async Task<ActionResult> UpdateClient(
        [FromRoute()] SystemTypeWhereUniqueInput uniqueId,
        [FromBody()] ClientWhereUniqueInput[] clientsId
    )
    {
        try
        {
            await _service.UpdateClient(uniqueId, clientsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
