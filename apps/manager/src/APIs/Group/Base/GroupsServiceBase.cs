using Manager.APIs;
using Manager.APIs.Common;
using Manager.APIs.Dtos;
using Manager.APIs.Errors;
using Manager.APIs.Extensions;
using Manager.Infrastructure;
using Manager.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.APIs;

public abstract class GroupsServiceBase : IGroupsService
{
    protected readonly ManagerDbContext _context;

    public GroupsServiceBase(ManagerDbContext context)
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
        if (createDto.Employees != null)
        {
            group.Employees = await _context
                .Employees.Where(employee =>
                    createDto.Employees.Select(t => t.Id).Contains(employee.Id)
                )
                .ToListAsync();
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
            .Groups.Include(x => x.Employees)
            .ApplyWhere(findManyArgs.Where)
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

        if (updateDto.Employees != null)
        {
            group.Employees = await _context
                .Employees.Where(employee =>
                    updateDto.Employees.Select(t => t).Contains(employee.Id)
                )
                .ToListAsync();
        }

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

    /// <summary>
    /// Connect multiple employees records to group
    /// </summary>
    public async Task ConnectEmployees(
        GroupWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    )
    {
        var parent = await _context
            .Groups.Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var employees = await _context
            .Employees.Where(t => employeesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (employees.Count == 0)
        {
            throw new NotFoundException();
        }

        var employeesToConnect = employees.Except(parent.Employees);

        foreach (var employee in employeesToConnect)
        {
            parent.Employees.Add(employee);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple employees records from group
    /// </summary>
    public async Task DisconnectEmployees(
        GroupWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    )
    {
        var parent = await _context
            .Groups.Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var employees = await _context
            .Employees.Where(t => employeesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var employee in employees)
        {
            parent.Employees?.Remove(employee);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple employees records for group
    /// </summary>
    public async Task<List<Employee>> FindEmployees(
        GroupWhereUniqueInput uniqueId,
        EmployeeFindManyArgs groupFindManyArgs
    )
    {
        var employees = await _context
            .Employees.Where(m => m.GroupsId == uniqueId.Id)
            .ApplyWhere(groupFindManyArgs.Where)
            .ApplySkip(groupFindManyArgs.Skip)
            .ApplyTake(groupFindManyArgs.Take)
            .ApplyOrderBy(groupFindManyArgs.SortBy)
            .ToListAsync();

        return employees.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple employees records for group
    /// </summary>
    public async Task UpdateEmployees(
        GroupWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    )
    {
        var group = await _context
            .Groups.Include(t => t.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (group == null)
        {
            throw new NotFoundException();
        }

        var employees = await _context
            .Employees.Where(a => employeesId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (employees.Count == 0)
        {
            throw new NotFoundException();
        }

        group.Employees = employees;
        await _context.SaveChangesAsync();
    }
}
