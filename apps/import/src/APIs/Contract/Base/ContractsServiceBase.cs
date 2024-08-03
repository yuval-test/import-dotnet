using Import.APIs;
using Import.APIs.Common;
using Import.APIs.Dtos;
using Import.APIs.Errors;
using Import.APIs.Extensions;
using Import.Infrastructure;
using Import.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Import.APIs;

public abstract class ContractsServiceBase : IContractsService
{
    protected readonly ImportDbContext _context;

    public ContractsServiceBase(ImportDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Contract
    /// </summary>
    public async Task<Contract> CreateContract(ContractCreateInput createDto)
    {
        var contract = new ContractDbModel
        {
            ClientId = createDto.ClientId,
            Created = createDto.Created,
            Createdby = createDto.Createdby,
            ExpireDate = createDto.ExpireDate,
            Modified = createDto.Modified,
            Modifiedby = createDto.Modifiedby,
            StartDate = createDto.StartDate
        };

        if (createDto.Id != null)
        {
            contract.Id = createDto.Id;
        }
        if (createDto.SubscriptionType != null)
        {
            contract.SubscriptionType = await _context
                .SubscriptionTypes.Where(subscriptionType =>
                    createDto.SubscriptionType.Id == subscriptionType.Id
                )
                .FirstOrDefaultAsync();
        }

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ContractDbModel>(contract.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Contract
    /// </summary>
    public async Task DeleteContract(ContractWhereUniqueInput uniqueId)
    {
        var contract = await _context.Contracts.FindAsync(uniqueId.Id);
        if (contract == null)
        {
            throw new NotFoundException();
        }

        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Contracts
    /// </summary>
    public async Task<List<Contract>> Contracts(ContractFindManyArgs findManyArgs)
    {
        var contracts = await _context
            .Contracts.Include(x => x.SubscriptionType)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return contracts.ConvertAll(contract => contract.ToDto());
    }

    /// <summary>
    /// Meta data about Contract records
    /// </summary>
    public async Task<MetadataDto> ContractsMeta(ContractFindManyArgs findManyArgs)
    {
        var count = await _context.Contracts.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Contract
    /// </summary>
    public async Task<Contract> Contract(ContractWhereUniqueInput uniqueId)
    {
        var contracts = await this.Contracts(
            new ContractFindManyArgs { Where = new ContractWhereInput { Id = uniqueId.Id } }
        );
        var contract = contracts.FirstOrDefault();
        if (contract == null)
        {
            throw new NotFoundException();
        }

        return contract;
    }

    /// <summary>
    /// Update one Contract
    /// </summary>
    public async Task UpdateContract(
        ContractWhereUniqueInput uniqueId,
        ContractUpdateInput updateDto
    )
    {
        var contract = updateDto.ToModel(uniqueId);

        _context.Entry(contract).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Contracts.Any(e => e.Id == contract.Id))
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
    /// Get a Subscription Type record for Contract
    /// </summary>
    public async Task<SubscriptionType> GetSubscriptionType(ContractWhereUniqueInput uniqueId)
    {
        var contract = await _context
            .Contracts.Where(contract => contract.Id == uniqueId.Id)
            .Include(contract => contract.SubscriptionType)
            .FirstOrDefaultAsync();
        if (contract == null)
        {
            throw new NotFoundException();
        }
        return contract.SubscriptionType.ToDto();
    }
}
