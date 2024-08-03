using Import.APIs.Dtos;
using Import.Infrastructure.Models;

namespace Import.APIs.Extensions;

public static class ContractsExtensions
{
    public static Contract ToDto(this ContractDbModel model)
    {
        return new Contract
        {
            ClientId = model.ClientId,
            Created = model.Created,
            Createdby = model.Createdby,
            ExpireDate = model.ExpireDate,
            Id = model.Id,
            Modified = model.Modified,
            Modifiedby = model.Modifiedby,
            RealtedSubscriptionType = model.RealtedSubscriptionTypeId,
            StartDate = model.StartDate,
        };
    }

    public static ContractDbModel ToModel(
        this ContractUpdateInput updateDto,
        ContractWhereUniqueInput uniqueId
    )
    {
        var contract = new ContractDbModel
        {
            Id = uniqueId.Id,
            ExpireDate = updateDto.ExpireDate,
            Modified = updateDto.Modified,
            Modifiedby = updateDto.Modifiedby
        };

        if (updateDto.ClientId != null)
        {
            contract.ClientId = updateDto.ClientId;
        }
        if (updateDto.Created != null)
        {
            contract.Created = updateDto.Created.Value;
        }
        if (updateDto.Createdby != null)
        {
            contract.Createdby = updateDto.Createdby;
        }
        if (updateDto.RealtedSubscriptionType != null)
        {
            contract.RealtedSubscriptionTypeId = updateDto.RealtedSubscriptionType.Value;
        }
        if (updateDto.StartDate != null)
        {
            contract.StartDate = updateDto.StartDate.Value;
        }

        return contract;
    }
}
