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
    /// Get a Employees record for Employee
    /// </summary>
    public async Task<Employee> GetEmployees(EmployeeWhereUniqueInput uniqueId)
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

    /// <summary>
    /// Get a Supervisees record for Employee
    /// </summary>
    public async Task<Employee> GetSupervisees(EmployeeWhereUniqueInput uniqueId)
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
