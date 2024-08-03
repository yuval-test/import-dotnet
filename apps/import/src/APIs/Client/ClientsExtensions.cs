using Import.APIs.Dtos;
using Import.Infrastructure.Models;

namespace Import.APIs.Extensions;

public static class ClientsExtensions
{
    public static Client ToDto(this ClientDbModel model)
    {
        return new Client
        {
            CompanyName = model.CompanyName,
            Created = model.Created,
            Createdby = model.Createdby,
            DefaultCultureCode = model.DefaultCultureCode,
            Id = model.Id,
            IsSokClient = model.IsSokClient,
            Modified = model.Modified,
            Modifiedby = model.Modifiedby,
            NumberField = model.NumberField,
            SystemTypeField = model.SystemTypeFieldId,
        };
    }

    public static ClientDbModel ToModel(
        this ClientUpdateInput updateDto,
        ClientWhereUniqueInput uniqueId
    )
    {
        var client = new ClientDbModel
        {
            Id = uniqueId.Id,
            CompanyName = updateDto.CompanyName,
            DefaultCultureCode = updateDto.DefaultCultureCode,
            IsSokClient = updateDto.IsSokClient,
            Modified = updateDto.Modified,
            Modifiedby = updateDto.Modifiedby,
            NumberField = updateDto.NumberField
        };

        if (updateDto.Created != null)
        {
            client.Created = updateDto.Created.Value;
        }
        if (updateDto.Createdby != null)
        {
            client.Createdby = updateDto.Createdby;
        }
        if (updateDto.SystemTypeField != null)
        {
            client.SystemTypeFieldId = updateDto.SystemTypeField.Value;
        }

        return client;
    }
}
