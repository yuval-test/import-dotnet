using Import.APIs;
using Import.APIs.Common;
using Import.APIs.Dtos;
using Import.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ClientsControllerBase : ControllerBase
{
    protected readonly IClientsService _service;

    public ClientsControllerBase(IClientsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Client
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Client>> CreateClient(ClientCreateInput input)
    {
        var client = await _service.CreateClient(input);

        return CreatedAtAction(nameof(Client), new { id = client.Id }, client);
    }

    /// <summary>
    /// Delete one Client
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteClient([FromRoute()] ClientWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteClient(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Clients
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Client>>> Clients([FromQuery()] ClientFindManyArgs filter)
    {
        return Ok(await _service.Clients(filter));
    }

    /// <summary>
    /// Meta data about Client records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ClientsMeta(
        [FromQuery()] ClientFindManyArgs filter
    )
    {
        return Ok(await _service.ClientsMeta(filter));
    }

    /// <summary>
    /// Get one Client
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Client>> Client([FromRoute()] ClientWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Client(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Client
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateClient(
        [FromRoute()] ClientWhereUniqueInput uniqueId,
        [FromQuery()] ClientUpdateInput clientUpdateDto
    )
    {
        try
        {
            await _service.UpdateClient(uniqueId, clientUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a System Type Field record for Client
    /// </summary>
    [HttpGet("{Id}/systemTypes")]
    public async Task<ActionResult<List<SystemType>>> GetSystemTypeField(
        [FromRoute()] ClientWhereUniqueInput uniqueId
    )
    {
        var systemType = await _service.GetSystemTypeField(uniqueId);
        return Ok(systemType);
    }
}
