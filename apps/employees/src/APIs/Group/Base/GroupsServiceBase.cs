using Employees.APIs;
using Employees.APIs.Common;
using Employees.APIs.Dtos;
using Employees.APIs.Errors;
using Employees.APIs.Extensions;
using Employees.Infrastructure;
using Employees.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.APIs;

public abstract class GroupsServiceBase : IGroupsService
{
    protected readonly EmployeesDbContext _context;

    public GroupsServiceBase(EmployeesDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one group
    /// </summary>
    public async Task<Group> CreateGroup(GroupCreateInput createDto)
    {
        var group = new GroupDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            group.Id = createDto.Id;
        }

        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<GroupDbModel>(group.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one group
    /// </summary>
    public async Task DeleteGroup(GroupWhereUniqueInput uniqueId)
    {
        var group = await _context.Groups.FindAsync(uniqueId.Id);
        if (group == null)
        {
            throw new NotFoundException();
        }

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many groups
    /// </summary>
    public async Task<List<Group>> Groups(GroupFindManyArgs findManyArgs)
    {
        var groups = await _context
            .Groups.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return groups.ConvertAll(group => group.ToDto());
    }

    /// <summary>
    /// Meta data about group records
    /// </summary>
    public async Task<MetadataDto> GroupsMeta(GroupFindManyArgs findManyArgs)
    {
        var count = await _context.Groups.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one group
    /// </summary>
    public async Task<Group> Group(GroupWhereUniqueInput uniqueId)
    {
        var groups = await this.Groups(
            new GroupFindManyArgs { Where = new GroupWhereInput { Id = uniqueId.Id } }
        );
        var group = groups.FirstOrDefault();
        if (group == null)
        {
            throw new NotFoundException();
        }

        return group;
    }

    /// <summary>
    /// Update one group
    /// </summary>
    public async Task UpdateGroup(GroupWhereUniqueInput uniqueId, GroupUpdateInput updateDto)
    {
        var group = updateDto.ToModel(uniqueId);

        _context.Entry(group).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Groups.Any(e => e.Id == group.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
