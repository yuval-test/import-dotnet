using Manager.APIs;
using Manager.APIs.Common;
using Manager.APIs.Dtos;
using Manager.APIs.Errors;
using Manager.APIs.Extensions;
using Manager.Infrastructure;
using Manager.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager.APIs;

public abstract class EmployeesServiceBase : IEmployeesService
{
    protected readonly ManagerDbContext _context;

    public EmployeesServiceBase(ManagerDbContext context)
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
                    updateDto.Employees.Select(t => t).Contains(employee.Id)
                )
                .ToListAsync();
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
        EmployeeWhereUniqueInput[] employeesId
    )
    {
        var employee = await _context
            .Employees.Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (employee == null)
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

        var employeesToConnect = employees.Except(employee.Employees);

        foreach (var employee in employeesToConnect)
        {
            employee.Employees.Add(employee);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Employees records from Employee
    /// </summary>
    public async Task DisconnectEmployees(
        EmployeeWhereUniqueInput uniqueId,
        EmployeeWhereUniqueInput[] employeesId
    )
    {
        var employee = await _context
            .Employees.Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (employee == null)
        {
            throw new NotFoundException();
        }

        var employees = await _context
            .Employees.Where(t => employeesId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var employee in employees)
        {
            employee.Employees?.Remove(employee);
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
            .Employees.Where(m => m.Manager.Any(x => x.Id == uniqueId.Id))
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
        EmployeeWhereUniqueInput[] employeesId
    )
    {
        var employee = await _context
            .Employees.Include(t => t.Employees)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (employee == null)
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

        employee.Employees = employees;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Manager record for Employee
    /// </summary>
    public async Task<Employee> GetManager(EmployeeWhereUniqueInput uniqueId)
    {
        var employee = await _context
            .Employees.Where(employee => employee.Id == uniqueId.Id)
            .Include(employee => employee.Employees)
            .FirstOrDefaultAsync();
        if (employee == null)
        {
            throw new NotFoundException();
        }
        return employee.Employees.ToDto();
    }
}
