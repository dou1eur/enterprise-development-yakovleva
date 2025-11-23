using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Application.Interfaces.Repositories;

public interface IRepository<TEntity, TId>
{
    public Task<TEntity?> GetByIdAsync(TId id);
    public Task<List<TEntity>> GetAllAsync();
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<bool> DeleteAsync(TId id);
    public Task<bool> ExistsAsync(TId id);
}