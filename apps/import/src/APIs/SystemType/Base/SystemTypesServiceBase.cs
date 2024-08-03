using Import.APIs;
using Import.APIs.Common;
using Import.APIs.Dtos;
using Import.APIs.Errors;
using Import.APIs.Extensions;
using Import.Infrastructure;
using Import.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Import.APIs;

public abstract class SystemTypesServiceBase : ISystemTypesService
{
    protected readonly ImportDbContext _context;

    public SystemTypesServiceBase(ImportDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one System Type
    /// </summary>
    public async Task<SystemType> CreateSystemType(SystemTypeCreateInput createDto)
    {
        var systemType = new SystemTypeDbModel { Description = createDto.Description };

        if (createDto.Id != null)
        {
            systemType.Id = createDto.Id.Value;
        }
        if (createDto.Client != null)
        {
            systemType.Client = await _context
                .Clients.Where(client => createDto.Client.Select(t => t.Id).Contains(client.Id))
                .ToListAsync();
        }

        _context.SystemTypes.Add(systemType);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SystemTypeDbModel>(systemType.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one System Type
    /// </summary>
    public async Task DeleteSystemType(SystemTypeWhereUniqueInput uniqueId)
    {
        var systemType = await _context.SystemTypes.FindAsync(uniqueId.Id);
        if (systemType == null)
        {
            throw new NotFoundException();
        }

        _context.SystemTypes.Remove(systemType);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many SystemTypes
    /// </summary>
    public async Task<List<SystemType>> SystemTypes(SystemTypeFindManyArgs findManyArgs)
    {
        var systemTypes = await _context
            .SystemTypes.Include(x => x.Client)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return systemTypes.ConvertAll(systemType => systemType.ToDto());
    }

    /// <summary>
    /// Meta data about System Type records
    /// </summary>
    public async Task<MetadataDto> SystemTypesMeta(SystemTypeFindManyArgs findManyArgs)
    {
        var count = await _context.SystemTypes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one System Type
    /// </summary>
    public async Task<SystemType> SystemType(SystemTypeWhereUniqueInput uniqueId)
    {
        var systemTypes = await this.SystemTypes(
            new SystemTypeFindManyArgs { Where = new SystemTypeWhereInput { Id = uniqueId.Id } }
        );
        var systemType = systemTypes.FirstOrDefault();
        if (systemType == null)
        {
            throw new NotFoundException();
        }

        return systemType;
    }

    /// <summary>
    /// Update one System Type
    /// </summary>
    public async Task UpdateSystemType(
        SystemTypeWhereUniqueInput uniqueId,
        SystemTypeUpdateInput updateDto
    )
    {
        var systemType = updateDto.ToModel(uniqueId);

        if (updateDto.Client != null)
        {
            systemType.Client = await _context
                .Clients.Where(client => updateDto.Client.Select(t => t).Contains(client.Id))
                .ToListAsync();
        }

        _context.Entry(systemType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.SystemTypes.Any(e => e.Id == systemType.Id))
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
    /// Connect multiple Client records to System Type
    /// </summary>
    public async Task ConnectClient(
        SystemTypeWhereUniqueInput uniqueId,
        ClientWhereUniqueInput[] clientsId
    )
    {
        var systemType = await _context
            .SystemTypes.Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (systemType == null)
        {
            throw new NotFoundException();
        }

        var clients = await _context
            .Clients.Where(t => clientsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (clients.Count == 0)
        {
            throw new NotFoundException();
        }

        var clientsToConnect = clients.Except(systemType.Client);

        foreach (var client in clientsToConnect)
        {
            systemType.Client.Add(client);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Client records from System Type
    /// </summary>
    public async Task DisconnectClient(
        SystemTypeWhereUniqueInput uniqueId,
        ClientWhereUniqueInput[] clientsId
    )
    {
        var systemType = await _context
            .SystemTypes.Include(x => x.Client)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (systemType == null)
        {
            throw new NotFoundException();
        }

        var clients = await _context
            .Clients.Where(t => clientsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var client in clients)
        {
            systemType.Client?.Remove(client);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Client records for System Type
    /// </summary>
    public async Task<List<Client>> FindClient(
        SystemTypeWhereUniqueInput uniqueId,
        ClientFindManyArgs systemTypeFindManyArgs
    )
    {
        var clients = await _context
            .Clients.Where(m => m.SystemTypeFieldId == uniqueId.Id)
            .ApplyWhere(systemTypeFindManyArgs.Where)
            .ApplySkip(systemTypeFindManyArgs.Skip)
            .ApplyTake(systemTypeFindManyArgs.Take)
            .ApplyOrderBy(systemTypeFindManyArgs.SortBy)
            .ToListAsync();

        return clients.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Client records for System Type
    /// </summary>
    public async Task UpdateClient(
        SystemTypeWhereUniqueInput uniqueId,
        ClientWhereUniqueInput[] clientsId
    )
    {
        var systemType = await _context
            .SystemTypes.Include(t => t.Client)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (systemType == null)
        {
            throw new NotFoundException();
        }

        var clients = await _context
            .Clients.Where(a => clientsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (clients.Count == 0)
        {
            throw new NotFoundException();
        }

        systemType.Client = clients;
        await _context.SaveChangesAsync();
    }
}
