using Employees.APIs;
using Employees.APIs.Common;
using Employees.APIs.Dtos;
using Employees.APIs.Errors;
using Employees.APIs.Extensions;
using Employees.Infrastructure;
using Employees.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.APIs;

public abstract class EmployeesServiceBase : IEmployeesService
{
    protected readonly EmployeesDbContext _context;

    public EmployeesServiceBase(EmployeesDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Employee
    /// </summary>
    public async Task<Employee> CreateEmployee(EmployeeCreateInput createDto)
    {
        var employee = new EmployeeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Phone = createDto.Phone,
            StartDate = createDto.StartDate,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            employee.Id = createDto.Id;
        }
        if (createDto.Employees != null)
        {
            employee.Employees = await _context
                .Employees.Where(employee =>
                    createDto.Employees.Select(t => t.Id).Contains(employee.Id)
                )
                .ToListAsync();
        }

        if (createDto.Manager != null)
        {
            employee.Manager = await _context
                .Employees.Where(employee => createDto.Manager.Id == employee.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Supervisees != null)
        {
            employee.Supervisees = await _context
                .Employees.Where(employee =>
                    createDto.Supervisees.Select(t => t.Id).Contains(employee.Id)
                )
                .ToListAsync();
        }

        if (createDto.Supervisor != null)
        {
            employee.Supervisor = await _context
                .Employees.Where(employee => createDto.Supervisor.Id == employee.Id)
                .FirstOrDefaultAsync();
        }

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EmployeeDbModel>(employee.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Employee
    /// </summary>
    public async Task DeleteEmployee(EmployeeWhereUniqueInput uniqueId)
    {
        var employee = await _context.Employees.FindAsync(uniqueId.Id);
        if (employee == null)
        {
            throw new NotFoundException();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Employees
    /// </summary>
    public async Task<List<Employee>> Employees(EmployeeFindManyArgs findManyArgs)
    {
        var employees = await _context
            .Employees.Include(x => x.Employees)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return employees.ConvertAll(employee => employee.ToDto());
    }

    /// <summary>
    /// Meta data about Employee records
    /// </summary>
    public async Task<MetadataDto> EmployeesMeta(EmployeeFindManyArgs findManyArgs)
    {
        var count = await _context.Employees.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Employee
    /// </summary>
    public async Task<Employee> Employee(EmployeeWhereUniqueInput uniqueId)
    {
        var employees = await this.Employees(
            new EmployeeFindManyArgs { Where = new EmployeeWhereInput { Id = uniqueId.Id } }
        );
        var employee = employees.FirstOrDefault();
        if (employee == null)
        {
            throw new NotFoundException();
        }

        return employee;
    }

    /// <summary>
    /// Update one Employee
    /// </summary>
    public async Task UpdateEmployee(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeUpdateInput updateDto
    )
    {
        var employee = updateDto.ToModel(uniqueId);

        if (updateDto.Employees != null)
        {
            employee.Employees = await _context
                .Employees.Where(employee =>
                    updateDto.Employees.Select(t => t.Id).Contains(employee.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Manager != null)
        {
            employee.Manager = await _context
                .Employees.Where(employee => updateDto.Manager.Id == employee.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Supervisees != null)
        {
            employee.Supervisees = await _context
                .Employees.Where(employee =>
                    updateDto.Supervisees.Select(t => t.Id).Contains(employee.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Supervisor != null)
        {
            employee.Supervisor = await _context
                .Employees.Where(employee => updateDto.Supervisor.Id == employee.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Employees.Any(e => e.Id == employee.Id))
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
    /// Connect multiple Employees records to Employee
    /// </summary>
    public async Task ConnectEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Employees.Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Employees.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Employees);

        foreach (var child in childrenToConnect)
        {
            parent.Employees.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Employees records from Employee
    /// </summary>
    public async Task DisconnectEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Employees.Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Employees.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Employees?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Employees records for Employee
    /// </summary>
    public async Task<List<Employee>> FindEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeFindManyArgs employeeFindManyArgs
    )
    {
        var employees = await _context
            .Employees.Where(m => m.ManagerId == uniqueId.Id)
            .ApplyWhere(employeeFindManyArgs.Where)
            .ApplySkip(employeeFindManyArgs.Skip)
            .ApplyTake(employeeFindManyArgs.Take)
            .ApplyOrderBy(employeeFindManyArgs.SortBy)
            .ToListAsync();

        return employees.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Employees records for Employee
    /// </summary>
    public async Task UpdateEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] childrenIds
    )
    {
        var employee = await _context
            .Employees.Include(t => t.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (employee == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Employees.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        employee.Employees = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Manager record for Employee
    /// </summary>
    public async Task<Employee> GetManager(EmployeeWhereUniqueInput uniqueId)
    {
        var employee = await _context
            .Employees.Where(employee => employee.Id == uniqueId.Id)
            .Include(employee => employee.Manager)
            .FirstOrDefaultAsync();
        if (employee == null)
        {
            throw new NotFoundException();
        }
        return employee.Manager.ToDto();
    }

    /// <summary>
    /// Connect multiple Supervisees records to Employee
    /// </summary>
    public async Task ConnectSupervisees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Employees.Include(x => x.Supervisees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Employees.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Supervisees);

        foreach (var child in childrenToConnect)
        {
            parent.Supervisees.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Supervisees records from Employee
    /// </summary>
    public async Task DisconnectSupervisees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Employees.Include(x => x.Supervisees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Employees.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Supervisees?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Supervisees records for Employee
    /// </summary>
    public async Task<List<Employee>> FindSupervisees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeFindManyArgs employeeFindManyArgs
    )
    {
        var employees = await _context
            .Employees.Where(m => m.SupervisorId == uniqueId.Id)
            .ApplyWhere(employeeFindManyArgs.Where)
            .ApplySkip(employeeFindManyArgs.Skip)
            .ApplyTake(employeeFindManyArgs.Take)
            .ApplyOrderBy(employeeFindManyArgs.SortBy)
            .ToListAsync();

        return employees.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Supervisees records for Employee
    /// </summary>
    public async Task UpdateSupervisees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] childrenIds
    )
    {
        var employee = await _context
            .Employees.Include(t => t.Supervisees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (employee == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Employees.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        employee.Supervisees = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Supervisor record for Employee
    /// </summary>
    public async Task<Employee> GetSupervisor(EmployeeWhereUniqueInput uniqueId)
    {
        var employee = await _context
            .Employees.Where(employee => employee.Id == uniqueId.Id)
            .Include(employee => employee.Supervisor)
            .FirstOrDefaultAsync();
        if (employee == null)
        {
            throw new NotFoundException();
        }
        return employee.Supervisor.ToDto();
    }
}
