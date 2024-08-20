using Employees.APIs;
using Employees.Infrastructure;
using Employees.APIs.Dtos;
using Employees.Infrastructure.Models;
using Employees.APIs.Errors;
using Employees.APIs.Extensions;
using Employees.APIs.Common;
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
        if (createDto.Supervisees != null)
        {
            employee.Supervisees = await _context
                .Employees.Where(employee => createDto.Supervisees.Select(t => t.Id).Contains(employee.Id))
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
              .Employees
      .Include(x => x.Supervisees)
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

        var count = await _context
    .Employees
    .ApplyWhere(findManyArgs.Where)
    .CountAsync();

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
    public async Task UpdateEmployee(EmployeeWhereUniqueInput uniqueId, EmployeeUpdateInput updateDto)
    {
        var employee = updateDto.ToModel(uniqueId);

        if (updateDto.Supervisees != null)
        {
            employee.Supervisees = await _context
                .Employees.Where(employee => updateDto.Supervisees.Select(t => t).Contains(employee.Id))
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
    /// Connect multiple Supervisees records to Employee
    /// </summary>
    public async Task ConnectSupervisees(EmployeeWhereUniqueInput uniqueId, EmployeeWhereUniqueInput[] employeesId)
    {
        var parent = await _context
              .Employees.Include(x => x.Supervisees)
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

        var employeesToConnect = employees.Except(parent.Supervisees);

        foreach (var employee in employeesToConnect)
        {
            parent.Supervisees.Add(employee);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Supervisees records from Employee
    /// </summary>
    public async Task DisconnectSupervisees(EmployeeWhereUniqueInput uniqueId, EmployeeWhereUniqueInput[] employeesId)
    {
        var parent = await _context
                                .Employees.Include(x => x.Supervisees)
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
            parent.Supervisees?.Remove(employee);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Supervisees records for Employee
    /// </summary>
    public async Task<List<Employee>> FindSupervisees(EmployeeWhereUniqueInput uniqueId, EmployeeFindManyArgs employeeFindManyArgs)
    {
        var employees = await _context
              .Employees
      .Where(m => m.SupervisorId == uniqueId.Id)
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
    public async Task UpdateSupervisees(EmployeeWhereUniqueInput uniqueId, EmployeeWhereUniqueInput[] employeesId)
    {
        var employee = await _context
                .Employees.Include(t => t.Supervisees)
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

        employee.Supervisees = employees;
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
