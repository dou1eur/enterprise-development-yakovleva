using System;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Base interface for CRUD operations
/// </summary>
/// <typeparam name="TDto">The data transfer object type</typeparam>
/// <typeparam name="TCreateUpdateDto">The create/update data transfer object type</typeparam>
/// <typeparam name="TId">The identifier type</typeparam>
public interface IApplicationCRUDService<TDto, TCreateUpdateDto, TId>
{
    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="dto">Data transfer object containing creation details</param>
    /// <returns>The created entity data transfer object</returns>
    public TDto Create(TCreateUpdateDto dto);

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
    public System.Collections.Generic.List<TDto> GetAll();

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="dto">Data transfer object containing updated details</param>
    /// <param name="id">The unique identifier of the entity to update</param>
    /// <returns>The updated entity data transfer object</returns>
    public TDto Update(TCreateUpdateDto dto, TId id);

    /// <summary>
    /// Deletes an entity by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(TId id);
}