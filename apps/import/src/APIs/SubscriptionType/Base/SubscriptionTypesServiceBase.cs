using Import.APIs;
using Import.APIs.Common;
using Import.APIs.Dtos;
using Import.APIs.Errors;
using Import.APIs.Extensions;
using Import.Infrastructure;
using Import.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Import.APIs;

public abstract class SubscriptionTypesServiceBase : ISubscriptionTypesService
{
    protected readonly ImportDbContext _context;

    public SubscriptionTypesServiceBase(ImportDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Subscription Type
    /// </summary>
    public async Task<SubscriptionType> CreateSubscriptionType(
        SubscriptionTypeCreateInput createDto
    )
    {
        var subscriptionType = new SubscriptionTypeDbModel { Description = createDto.Description };

        if (createDto.Id != null)
        {
            subscriptionType.Id = createDto.Id.Value;
        }
        if (createDto.Contract != null)
        {
            subscriptionType.Contract = await _context
                .Contracts.Where(contract =>
                    createDto.Contract.Select(t => t.Id).Contains(contract.Id)
                )
                .ToListAsync();
        }

        _context.SubscriptionTypes.Add(subscriptionType);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SubscriptionTypeDbModel>(subscriptionType.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Subscription Type
    /// </summary>
    public async Task DeleteSubscriptionType(SubscriptionTypeWhereUniqueInput uniqueId)
    {
        var subscriptionType = await _context.SubscriptionTypes.FindAsync(uniqueId.Id);
        if (subscriptionType == null)
        {
            throw new NotFoundException();
        }

        _context.SubscriptionTypes.Remove(subscriptionType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many SubscriptionTypes
    /// </summary>
    public async Task<List<SubscriptionType>> SubscriptionTypes(
        SubscriptionTypeFindManyArgs findManyArgs
    )
    {
        var subscriptionTypes = await _context
            .SubscriptionTypes.Include(x => x.Contract)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return subscriptionTypes.ConvertAll(subscriptionType => subscriptionType.ToDto());
    }

    /// <summary>
    /// Meta data about Subscription Type records
    /// </summary>
    public async Task<MetadataDto> SubscriptionTypesMeta(SubscriptionTypeFindManyArgs findManyArgs)
    {
        var count = await _context.SubscriptionTypes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Subscription Type
    /// </summary>
    public async Task<SubscriptionType> SubscriptionType(SubscriptionTypeWhereUniqueInput uniqueId)
    {
        var subscriptionTypes = await this.SubscriptionTypes(
            new SubscriptionTypeFindManyArgs
            {
                Where = new SubscriptionTypeWhereInput { Id = uniqueId.Id }
            }
        );
        var subscriptionType = subscriptionTypes.FirstOrDefault();
        if (subscriptionType == null)
        {
            throw new NotFoundException();
        }

        return subscriptionType;
    }

    /// <summary>
    /// Update one Subscription Type
    /// </summary>
    public async Task UpdateSubscriptionType(
        SubscriptionTypeWhereUniqueInput uniqueId,
        SubscriptionTypeUpdateInput updateDto
    )
    {
        var subscriptionType = updateDto.ToModel(uniqueId);

        if (updateDto.Contract != null)
        {
            subscriptionType.Contract = await _context
                .Contracts.Where(contract =>
                    updateDto.Contract.Select(t => t).Contains(contract.Id)
                )
                .ToListAsync();
        }

        _context.Entry(subscriptionType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SubscriptionTypes.Any(e => e.Id == subscriptionType.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Contract records to Subscription Type
    /// </summary>
    public async Task ConnectContract(
        SubscriptionTypeWhereUniqueInput uniqueId,
        ContractWhereUniqueInput[] contractsId
    )
    {
        var subscriptionType = await _context
            .SubscriptionTypes.Include(x => x.Contract)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (subscriptionType == null)
        {
            throw new NotFoundException();
        }

        var contracts = await _context
            .Contracts.Where(t => contractsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (contracts.Count == 0)
        {
            throw new NotFoundException();
        }

        var contractsToConnect = contracts.Except(subscriptionType.Contract);

        foreach (var contract in contractsToConnect)
        {
            subscriptionType.Contract.Add(contract);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Contract records from Subscription Type
    /// </summary>
    public async Task DisconnectContract(
        SubscriptionTypeWhereUniqueInput uniqueId,
        ContractWhereUniqueInput[] contractsId
    )
    {
        var subscriptionType = await _context
            .SubscriptionTypes.Include(x => x.Contract)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (subscriptionType == null)
        {
            throw new NotFoundException();
        }

        var contracts = await _context
            .Contracts.Where(t => contractsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var contract in contracts)
        {
            subscriptionType.Contract?.Remove(contract);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Contract records for Subscription Type
    /// </summary>
    public async Task<List<Contract>> FindContract(
        SubscriptionTypeWhereUniqueInput uniqueId,
        ContractFindManyArgs subscriptionTypeFindManyArgs
    )
    {
        var contracts = await _context
            .Contracts.Where(m => m.SubscriptionTypeId == uniqueId.Id)
            .ApplyWhere(subscriptionTypeFindManyArgs.Where)
            .ApplySkip(subscriptionTypeFindManyArgs.Skip)
            .ApplyTake(subscriptionTypeFindManyArgs.Take)
            .ApplyOrderBy(subscriptionTypeFindManyArgs.SortBy)
            .ToListAsync();

        return contracts.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Contract records for Subscription Type
    /// </summary>
    public async Task UpdateContract(
        SubscriptionTypeWhereUniqueInput uniqueId,
        ContractWhereUniqueInput[] contractsId
    )
    {
        var subscriptionType = await _context
            .SubscriptionTypes.Include(t => t.Contract)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (subscriptionType == null)
        {
            throw new NotFoundException();
        }

        var contracts = await _context
            .Contracts.Where(a => contractsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (contracts.Count == 0)
        {
            throw new NotFoundException();
        }

        subscriptionType.Contract = contracts;
        await _context.SaveChangesAsync();
    }
}
