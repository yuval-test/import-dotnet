using Manager.APIs.Dtos;
using Manager.Infrastructure.Models;

namespace Manager.APIs.Extensions;

public static class GroupsExtensions
{
    public static Group ToDto(this GroupDbModel model)
    {
        return new Group
        {
            CreatedAt = model.CreatedAt,
            Employees = model.Employees?.Select(x => x.Id).ToList(),
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static GroupDbModel ToModel(
        this GroupUpdateInput updateDto,
        GroupWhereUniqueInput uniqueId
    )
    {
        var group = new GroupDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            group.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            group.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return group;
    }
}
