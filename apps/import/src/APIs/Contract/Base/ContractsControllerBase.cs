using Import.APIs;
using Import.APIs.Common;
using Import.APIs.Dtos;
using Import.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ContractsControllerBase : ControllerBase
{
    protected readonly IContractsService _service;

    public ContractsControllerBase(IContractsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Contract
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Contract>> CreateContract(ContractCreateInput input)
    {
        var contract = await _service.CreateContract(input);

        return CreatedAtAction(nameof(Contract), new { id = contract.Id }, contract);
    }

    /// <summary>
    /// Delete one Contract
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteContract([FromRoute()] ContractWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteContract(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Contracts
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Contract>>> Contracts(
        [FromQuery()] ContractFindManyArgs filter
    )
    {
        return Ok(await _service.Contracts(filter));
    }

    /// <summary>
    /// Meta data about Contract records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ContractsMeta(
        [FromQuery()] ContractFindManyArgs filter
    )
    {
        return Ok(await _service.ContractsMeta(filter));
    }

    /// <summary>
    /// Get one Contract
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Contract>> Contract(
        [FromRoute()] ContractWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Contract(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Contract
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateContract(
        [FromRoute()] ContractWhereUniqueInput uniqueId,
        [FromQuery()] ContractUpdateInput contractUpdateDto
    )
    {
        try
        {
            await _service.UpdateContract(uniqueId, contractUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Realted Subscription Type record for Contract
    /// </summary>
    [HttpGet("{Id}/subscriptionTypes")]
    public async Task<ActionResult<List<SubscriptionType>>> GetRealtedSubscriptionType(
        [FromRoute()] ContractWhereUniqueInput uniqueId
    )
    {
        var subscriptionType = await _service.GetRealtedSubscriptionType(uniqueId);
        return Ok(subscriptionType);
    }
}
