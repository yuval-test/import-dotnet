using Import.APIs;
using Import.APIs.Common;
using Import.APIs.Dtos;
using Import.APIs.Errors;
using Import.APIs.Extensions;
using Import.Infrastructure;
using Import.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Import.APIs;

public abstract class ClientsServiceBase : IClientsService
{
    protected readonly ImportDbContext _context;

    public ClientsServiceBase(ImportDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Client
    /// </summary>
    public async Task<Client> CreateClient(ClientCreateInput createDto)
    {
        var client = new ClientDbModel
        {
            CompanyName = createDto.CompanyName,
            Created = createDto.Created,
            Createdby = createDto.Createdby,
            DefaultCultureCode = createDto.DefaultCultureCode,
            IsSokClient = createDto.IsSokClient,
            Modified = createDto.Modified,
            Modifiedby = createDto.Modifiedby,
            NumberField = createDto.NumberField
        };

        if (createDto.Id != null)
        {
            client.Id = createDto.Id;
        }
        if (createDto.SystemTypeField != null)
        {
            client.SystemTypeField = await _context
                .SystemTypes.Where(systemType => createDto.SystemTypeField.Id == systemType.Id)
                .FirstOrDefaultAsync();
        }

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ClientDbModel>(client.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Client
    /// </summary>
    public async Task DeleteClient(ClientWhereUniqueInput uniqueId)
    {
        var client = await _context.Clients.FindAsync(uniqueId.Id);
        if (client == null)
        {
            throw new NotFoundException();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Clients
    /// </summary>
    public async Task<List<Client>> Clients(ClientFindManyArgs findManyArgs)
    {
        var clients = await _context
            .Clients.Include(x => x.SystemTypeField)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return clients.ConvertAll(client => client.ToDto());
    }

    /// <summary>
    /// Meta data about Client records
    /// </summary>
    public async Task<MetadataDto> ClientsMeta(ClientFindManyArgs findManyArgs)
    {
        var count = await _context.Clients.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Client
    /// </summary>
    public async Task<Client> Client(ClientWhereUniqueInput uniqueId)
    {
        var clients = await this.Clients(
            new ClientFindManyArgs { Where = new ClientWhereInput { Id = uniqueId.Id } }
        );
        var client = clients.FirstOrDefault();
        if (client == null)
        {
            throw new NotFoundException();
        }

        return client;
    }

    /// <summary>
    /// Update one Client
    /// </summary>
    public async Task UpdateClient(ClientWhereUniqueInput uniqueId, ClientUpdateInput updateDto)
    {
        var client = updateDto.ToModel(uniqueId);

        _context.Entry(client).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Clients.Any(e => e.Id == client.Id))
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
    /// Get a System Type Field record for Client
    /// </summary>
    public async Task<SystemType> GetSystemTypeField(ClientWhereUniqueInput uniqueId)
    {
        var client = await _context
            .Clients.Where(client => client.Id == uniqueId.Id)
            .Include(client => client.SystemTypeField)
            .FirstOrDefaultAsync();
        if (client == null)
        {
            throw new NotFoundException();
        }
        return client.SystemTypeField.ToDto();
    }
}
