using Import.APIs.Dtos;
using Import.Infrastructure.Models;

namespace Import.APIs.Extensions;

public static class SubscriptionTypesExtensions
{
    public static SubscriptionType ToDto(this SubscriptionTypeDbModel model)
    {
        return new SubscriptionType
        {
            Contract = model.Contract?.Select(x => x.Id).ToList(),
            Description = model.Description,
            Id = model.Id,
        };
    }

    public static SubscriptionTypeDbModel ToModel(
        this SubscriptionTypeUpdateInput updateDto,
        SubscriptionTypeWhereUniqueInput uniqueId
    )
    {
        var subscriptionType = new SubscriptionTypeDbModel { Id = uniqueId.Id };

        if (updateDto.Description != null)
        {
            subscriptionType.Description = updateDto.Description;
        }

        return subscriptionType;
    }
}
