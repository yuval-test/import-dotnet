using Employees.APIs.Common;
using Employees.APIs.Dtos;

namespace Employees.APIs;

public interface IGroupsService
{
    /// <summary>
    /// Create one group
    /// </summary>
    public Task<Group> CreateGroup(GroupCreateInput group);

    /// <summary>
    /// Delete one group
    /// </summary>
    public Task DeleteGroup(GroupWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many groups
    /// </summary>
    public Task<List<Group>> Groups(GroupFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about group records
    /// </summary>
    public Task<MetadataDto> GroupsMeta(GroupFindManyArgs findManyArgs);

    /// <summary>
    /// Get one group
    /// </summary>
    public Task<Group> Group(GroupWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one group
    /// </summary>
    public Task UpdateGroup(GroupWhereUniqueInput uniqueId, GroupUpdateInput updateDto);
}
