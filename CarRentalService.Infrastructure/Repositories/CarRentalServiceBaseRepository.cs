using CarRentalService.Infrastructure.Data;
using CarRentalService.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// Base repository implementation for Entity Framework Core
/// Provides common CRUD operations for all entity types
/// </summary>
/// <typeparam name="TEntity">The type of entity</typeparam>
/// <typeparam name="TId">The type of entity identifier</typeparam>
public abstract class CarRentalServiceBaseRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    protected readonly CarRentalDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of CarRentalServiceBaseRepository
    /// </summary>
    /// <param name="dbContext">The database context</param>
    protected CarRentalServiceBaseRepository(CarRentalDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated data</param>
    /// <returns>The updated entity</returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var result = _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public virtual async Task<bool> DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Checks if an entity with the specified identifier exists
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if entity exists, otherwise false</returns>
    public virtual async Task<bool> ExistsAsync(TId id)
    {
        return await GetByIdAsync(id) != null;
    }
}