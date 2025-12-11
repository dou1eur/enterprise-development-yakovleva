using AutoMapper;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// Base repository implementation for Entity Framework Core
/// Provides common CRUD operations for all entity types
/// </summary>
/// <typeparam name="TEntity">The type of entity</typeparam>
/// <typeparam name="TDomain">The type of domain model</typeparam>
/// <typeparam name="TId">The type of entity identifier</typeparam>
public abstract class CarRentalServiceBaseRepository<TEntity, TDomain, TId>(
    CarRentalDbContext dbContext,
    IMapper mapper)
    : IRepository<TDomain, TId>
    where TEntity : class
    where TDomain : class
    where TId : struct
{
    protected readonly CarRentalDbContext _dbContext = dbContext;
    protected readonly IMapper _mapper = mapper;
    protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public virtual async Task<TDomain?> GetByIdAsync(TId id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity == null ? null : _mapper.Map<TDomain>(entity);
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public virtual async Task<List<TDomain>> GetAllAsync()
    {
        var entities = await _dbSet.AsNoTracking().ToListAsync();
        return _mapper.Map<List<TDomain>>(entities);
    }

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="domain">The domain entity to add</param>
    /// <returns>The added domain entity</returns>
    public virtual async Task<TDomain> AddAsync(TDomain domain)
    {
        var entity = _mapper.Map<TEntity>(domain);
        var result = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TDomain>(result.Entity);
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="domain">The domain entity with updated data</param>
    /// <returns>The updated domain entity</returns>
    public virtual async Task<TDomain> UpdateAsync(TDomain domain)
    {
        var entity = _mapper.Map<TEntity>(domain);
        var result = _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TDomain>(result.Entity);
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

        var dbEntity = _mapper.Map<TEntity>(entity);
        _dbSet.Remove(dbEntity);
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