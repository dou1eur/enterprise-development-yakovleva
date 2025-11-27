using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Base interface for read-only operations
/// </summary>
/// <typeparam name="TDto">The data transfer object type</typeparam>
/// <typeparam name="TId">The identifier type</typeparam>
public interface IApplicationReadService<TDto, TId>
{
    /// <summary>
    /// Retrieves an entity by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <returns>The entity data transfer object</returns>
    public TDto Get(TId id);

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entity data transfer objects</returns>
    public List<TDto> GetAll();
}