using Import.APIs.Dtos;
using Import.Infrastructure.Models;

namespace Import.APIs.Extensions;

public static class UsersExtensions
{
    public static User ToDto(this UserDbModel model)
    {
        return new User
        {
            ClientId = model.ClientId,
            Created = model.Created,
            Createdby = model.Createdby,
            Email = model.Email,
            Id = model.Id,
            IsDisabled = model.IsDisabled,
            IsMainUser = model.IsMainUser,
            Modified = model.Modified,
            Modifiedby = model.Modifiedby,
            TrustedPhoneNumber = model.TrustedPhoneNumber,
        };
    }

    public static UserDbModel ToModel(this UserUpdateInput updateDto, UserWhereUniqueInput uniqueId)
    {
        var user = new UserDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            Modified = updateDto.Modified,
            Modifiedby = updateDto.Modifiedby,
            TrustedPhoneNumber = updateDto.TrustedPhoneNumber
        };

        if (updateDto.ClientId != null)
        {
            user.ClientId = updateDto.ClientId;
        }
        if (updateDto.Created != null)
        {
            user.Created = updateDto.Created.Value;
        }
        if (updateDto.Createdby != null)
        {
            user.Createdby = updateDto.Createdby;
        }
        if (updateDto.IsDisabled != null)
        {
            user.IsDisabled = updateDto.IsDisabled.Value;
        }
        if (updateDto.IsMainUser != null)
        {
            user.IsMainUser = updateDto.IsMainUser.Value;
        }

        return user;
    }
}
