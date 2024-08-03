using Import.APIs;
using Import.APIs.Common;
using Import.APIs.Dtos;
using Import.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Import.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SubscriptionTypesControllerBase : ControllerBase
{
    protected readonly ISubscriptionTypesService _service;

    public SubscriptionTypesControllerBase(ISubscriptionTypesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Subscription Type
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<SubscriptionType>> CreateSubscriptionType(
        SubscriptionTypeCreateInput input
    )
    {
        var subscriptionType = await _service.CreateSubscriptionType(input);

        return CreatedAtAction(
            nameof(SubscriptionType),
            new { id = subscriptionType.Id },
            subscriptionType
        );
    }

    /// <summary>
    /// Delete one Subscription Type
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSubscriptionType(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteSubscriptionType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many SubscriptionTypes
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<SubscriptionType>>> SubscriptionTypes(
        [FromQuery()] SubscriptionTypeFindManyArgs filter
    )
    {
        return Ok(await _service.SubscriptionTypes(filter));
    }

    /// <summary>
    /// Meta data about Subscription Type records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SubscriptionTypesMeta(
        [FromQuery()] SubscriptionTypeFindManyArgs filter
    )
    {
        return Ok(await _service.SubscriptionTypesMeta(filter));
    }

    /// <summary>
    /// Get one Subscription Type
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<SubscriptionType>> SubscriptionType(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.SubscriptionType(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Subscription Type
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSubscriptionType(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromQuery()] SubscriptionTypeUpdateInput subscriptionTypeUpdateDto
    )
    {
        try
        {
            await _service.UpdateSubscriptionType(uniqueId, subscriptionTypeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Contract records to Subscription Type
    /// </summary>
    [HttpPost("{Id}/contracts")]
    public async Task<ActionResult> ConnectContract(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromQuery()] ContractWhereUniqueInput[] contractsId
    )
    {
        try
        {
            await _service.ConnectContract(uniqueId, contractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Contract records from Subscription Type
    /// </summary>
    [HttpDelete("{Id}/contracts")]
    public async Task<ActionResult> DisconnectContract(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromBody()] ContractWhereUniqueInput[] contractsId
    )
    {
        try
        {
            await _service.DisconnectContract(uniqueId, contractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Contract records for Subscription Type
    /// </summary>
    [HttpGet("{Id}/contracts")]
    public async Task<ActionResult<List<Contract>>> FindContract(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromQuery()] ContractFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindContract(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Contract records for Subscription Type
    /// </summary>
    [HttpPatch("{Id}/contracts")]
    public async Task<ActionResult> UpdateContract(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromBody()] ContractWhereUniqueInput[] contractsId
    )
    {
        try
        {
            await _service.UpdateContract(uniqueId, contractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Other Contracts records to Subscription Type
    /// </summary>
    [HttpPost("{Id}/contracts")]
    public async Task<ActionResult> ConnectOtherContracts(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromQuery()] ContractWhereUniqueInput[] contractsId
    )
    {
        try
        {
            await _service.ConnectContract(uniqueId, contractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Other Contracts records from Subscription Type
    /// </summary>
    [HttpDelete("{Id}/contracts")]
    public async Task<ActionResult> DisconnectOtherContracts(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromBody()] ContractWhereUniqueInput[] contractsId
    )
    {
        try
        {
            await _service.DisconnectContract(uniqueId, contractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Other Contracts records for Subscription Type
    /// </summary>
    [HttpGet("{Id}/contracts")]
    public async Task<ActionResult<List<Contract>>> FindOtherContracts(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromQuery()] ContractFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindContract(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Other Contracts records for Subscription Type
    /// </summary>
    [HttpPatch("{Id}/contracts")]
    public async Task<ActionResult> UpdateOtherContracts(
        [FromRoute()] SubscriptionTypeWhereUniqueInput uniqueId,
        [FromBody()] ContractWhereUniqueInput[] contractsId
    )
    {
        try
        {
            await _service.UpdateContract(uniqueId, contractsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
