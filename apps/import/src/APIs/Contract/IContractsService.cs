using Import.APIs.Common;
using Import.APIs.Dtos;

namespace Import.APIs;

public interface IContractsService
{
    /// <summary>
    /// Create one Contract
    /// </summary>
    public Task<Contract> CreateContract(ContractCreateInput contract);

    /// <summary>
    /// Delete one Contract
    /// </summary>
    public Task DeleteContract(ContractWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Contracts
    /// </summary>
    public Task<List<Contract>> Contracts(ContractFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Contract records
    /// </summary>
    public Task<MetadataDto> ContractsMeta(ContractFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Contract
    /// </summary>
    public Task<Contract> Contract(ContractWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Contract
    /// </summary>
    public Task UpdateContract(ContractWhereUniqueInput uniqueId, ContractUpdateInput updateDto);

    /// <summary>
    /// Get a Subscription Type record for Contract
    /// </summary>
    public Task<SubscriptionType> GetSubscriptionType(ContractWhereUniqueInput uniqueId);
}
