using Manager.APIs.Common;
using Manager.APIs.Dtos;

namespace Manager.APIs;

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

    /// <summary>
    /// Connect multiple employees records to group
    /// </summary>
    public Task ConnectEmployees(
        GroupWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );

    /// <summary>
    /// Disconnect multiple employees records from group
    /// </summary>
    public Task DisconnectEmployees(
        GroupWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );

    /// <summary>
    /// Find multiple employees records for group
    /// </summary>
    public Task<List<Employee>> FindEmployees(
        GroupWhereUniqueInput uniqueId,
        EmployeeFindManyArgs EmployeeFindManyArgs
    );

    /// <summary>
    /// Update multiple employees records for group
    /// </summary>
    public Task UpdateEmployees(
        GroupWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    );
}
