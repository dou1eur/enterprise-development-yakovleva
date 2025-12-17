using CarRentalService.Infrastructure.Data;
using CarRentalService.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// Base repository implementation for Entity Framework Core
/// Provides common CRUD operations for all domain entities with support for includes
/// </summary>
/// <typeparam name="TEntity">The type of domain entity</typeparam>
/// <typeparam name="TId">The type of entity identifier</typeparam>
/// <param name="dbContext">The database context</param>
public abstract class BaseRepository<TEntity, TId>(CarRentalDbContext dbContext)
    : IRepository<TEntity, TId>
    where TEntity : class
    where TId : struct
{
    protected CarRentalDbContext DbContext { get; } = dbContext;

    /// <summary>
    /// The DbSet for the entity type
    /// </summary>
    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

    /// <summary>
    /// Gets the base query for the entity with optional includes
    /// </summary>
    /// <returns>Queryable with includes applied</returns>
    protected virtual IQueryable<TEntity> GetBaseQuery() => DbSet;

    /// <summary>
    /// Gets the query for read-only operations (no tracking)
    /// </summary>
    /// <returns>Queryable with includes applied and no tracking</returns>
    protected virtual IQueryable<TEntity> GetBaseQueryAsNoTracking() =>
        GetBaseQuery().AsNoTracking();

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public virtual async Task<TEntity?> GetByIdAsync(TId id) =>
        await GetBaseQuery()
            .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == (Guid)(object)id);
    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public virtual async Task<List<TEntity>> GetAllAsync() =>
        await GetBaseQueryAsNoTracking().ToListAsync();

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await DbSet.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated data</param>
    /// <returns>The updated entity</returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var result = DbSet.Update(entity);
        await DbContext.SaveChangesAsync();
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
        if (entity is null) return false;

        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync();
        return true;
    }
}