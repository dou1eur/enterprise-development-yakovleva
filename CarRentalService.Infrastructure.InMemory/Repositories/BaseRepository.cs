using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.InMemory.Repositories;

/// <summary>
/// Base repository implementation for in-memory data storage
/// Provides common CRUD operations for all entity types
/// </summary>
/// <typeparam name="TEntity">The type of entity</typeparam>
/// <typeparam name="TId">The type of entity identifier</typeparam>
public abstract class BaseRepository<TEntity, TId> where TEntity : class
{
    /// <summary>
    /// In-memory storage for entities
    /// </summary>
    protected readonly List<TEntity> _entities = new();

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public virtual Task<TEntity?> GetByIdAsync(TId id)
    {
        var entity = _entities.FirstOrDefault(e => GetId(e)?.Equals(id) == true);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return Task.FromResult(_entities.ToList());
    }

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    public virtual Task<TEntity> AddAsync(TEntity entity)
    {
        _entities.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated data</param>
    /// <returns>The updated entity</returns>
    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        var id = GetId(entity);
        var existing = _entities.FirstOrDefault(e => GetId(e)?.Equals(id) == true);
        if (existing != null)
        {
            _entities.Remove(existing);
            _entities.Add(entity);
        }
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Deletes an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public virtual Task<bool> DeleteAsync(TId id)
    {
        var entity = _entities.FirstOrDefault(e => GetId(e)?.Equals(id) == true);
        if (entity != null)
        {
            _entities.Remove(entity);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    /// <summary>
    /// Checks if an entity with the specified identifier exists
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if entity exists, otherwise false</returns>
    public virtual Task<bool> ExistsAsync(TId id)
    {
        var exists = _entities.Any(e => GetId(e)?.Equals(id) == true);
        return Task.FromResult(exists);
    }

    /// <summary>
    /// Extracts the identifier from an entity
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>The entity identifier</returns>
    protected abstract object? GetId(TEntity entity);
}