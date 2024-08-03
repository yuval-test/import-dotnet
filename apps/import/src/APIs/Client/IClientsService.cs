using Import.APIs.Common;
using Import.APIs.Dtos;

namespace Import.APIs;

public interface IClientsService
{
    /// <summary>
    /// Create one Client
    /// </summary>
    public Task<Client> CreateClient(ClientCreateInput client);

    /// <summary>
    /// Delete one Client
    /// </summary>
    public Task DeleteClient(ClientWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Clients
    /// </summary>
    public Task<List<Client>> Clients(ClientFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Client records
    /// </summary>
    public Task<MetadataDto> ClientsMeta(ClientFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Client
    /// </summary>
    public Task<Client> Client(ClientWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Client
    /// </summary>
    public Task UpdateClient(ClientWhereUniqueInput uniqueId, ClientUpdateInput updateDto);

    /// <summary>
    /// Get a System Type Field record for Client
    /// </summary>
    public Task<SystemType> GetSystemTypeField(ClientWhereUniqueInput uniqueId);
}
