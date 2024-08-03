using Import.APIs.Common;
using Import.APIs.Dtos;

namespace Import.APIs;

public interface ISubscriptionTypesService
{
    /// <summary>
    /// Create one Subscription Type
    /// </summary>
    public Task<SubscriptionType> CreateSubscriptionType(
        SubscriptionTypeCreateInput subscriptiontype
    );

    /// <summary>
    /// Delete one Subscription Type
    /// </summary>
    public Task DeleteSubscriptionType(SubscriptionTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many SubscriptionTypes
    /// </summary>
    public Task<List<SubscriptionType>> SubscriptionTypes(
        SubscriptionTypeFindManyArgs findManyArgs
    );

    /// <summary>
    /// Meta data about Subscription Type records
    /// </summary>
    public Task<MetadataDto> SubscriptionTypesMeta(SubscriptionTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Subscription Type
    /// </summary>
    public Task<SubscriptionType> SubscriptionType(SubscriptionTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Subscription Type
    /// </summary>
    public Task UpdateSubscriptionType(
        SubscriptionTypeWhereUniqueInput uniqueId,
        SubscriptionTypeUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Contract records to Subscription Type
    /// </summary>
    public Task ConnectContract(
        SubscriptionTypeWhereUniqueInput uniqueId,
        ContractWhereUniqueInput[] contractsId
    );

    /// <summary>
    /// Disconnect multiple Contract records from Subscription Type
    /// </summary>
    public Task DisconnectContract(
        SubscriptionTypeWhereUniqueInput uniqueId,
        ContractWhereUniqueInput[] contractsId
    );

    /// <summary>
    /// Find multiple Contract records for Subscription Type
    /// </summary>
    public Task<List<Contract>> FindContract(
        SubscriptionTypeWhereUniqueInput uniqueId,
        ContractFindManyArgs ContractFindManyArgs
    );

    /// <summary>
    /// Update multiple Contract records for Subscription Type
    /// </summary>
    public Task UpdateContract(
        SubscriptionTypeWhereUniqueInput uniqueId,
        ContractWhereUniqueInput[] contractsId
    );
}
