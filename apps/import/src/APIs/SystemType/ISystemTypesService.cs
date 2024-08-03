using Import.APIs.Common;
using Import.APIs.Dtos;

namespace Import.APIs;

public interface ISystemTypesService
{
    /// <summary>
    /// Create one System Type
    /// </summary>
    public Task<SystemType> CreateSystemType(SystemTypeCreateInput systemtype);

    /// <summary>
    /// Delete one System Type
    /// </summary>
    public Task DeleteSystemType(SystemTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many SystemTypes
    /// </summary>
    public Task<List<SystemType>> SystemTypes(SystemTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about System Type records
    /// </summary>
    public Task<MetadataDto> SystemTypesMeta(SystemTypeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one System Type
    /// </summary>
    public Task<SystemType> SystemType(SystemTypeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one System Type
    /// </summary>
    public Task UpdateSystemType(
        SystemTypeWhereUniqueInput uniqueId,
        SystemTypeUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Client records to System Type
    /// </summary>
    public Task ConnectClient(
        SystemTypeWhereUniqueInput uniqueId,
        ClientWhereUniqueInput[] clientsId
    );

    /// <summary>
    /// Disconnect multiple Client records from System Type
    /// </summary>
    public Task DisconnectClient(
        SystemTypeWhereUniqueInput uniqueId,
        ClientWhereUniqueInput[] clientsId
    );

    /// <summary>
    /// Find multiple Client records for System Type
    /// </summary>
    public Task<List<Client>> FindClient(
        SystemTypeWhereUniqueInput uniqueId,
        ClientFindManyArgs ClientFindManyArgs
    );

    /// <summary>
    /// Update multiple Client records for System Type
    /// </summary>
    public Task UpdateClient(
        SystemTypeWhereUniqueInput uniqueId,
        ClientWhereUniqueInput[] clientsId
    );
}
