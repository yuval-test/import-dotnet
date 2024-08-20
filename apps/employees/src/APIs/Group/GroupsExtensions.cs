using Employees.APIs.Dtos;
using Employees.Infrastructure.Models;

namespace Employees.APIs.Extensions;

public static class GroupsExtensions
{
    public static Group ToDto(this GroupDbModel model)
    {
        return new Group
        {
            CreatedAt = model.CreatedAt,
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
