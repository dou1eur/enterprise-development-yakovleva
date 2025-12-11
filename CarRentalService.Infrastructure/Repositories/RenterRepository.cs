using AutoMapper;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for renter entities
/// Handles data access for renters with mapping between domain and entity models
/// </summary>
public class RenterRepository(
    CarRentalDbContext dbContext,
    IMapper mapper)
    : CarRentalServiceBaseRepository<RenterEntity, Renter, Guid>(dbContext, mapper),
      IRenterRepository
{
    /// <summary>
    /// Retrieves a renter by their driver's license number
    /// </summary>
    /// <param name="licenseNumber">The driver's license number</param>
    /// <returns>The renter if found, otherwise null</returns>
    public async Task<Renter?> GetByLicenseNumberAsync(string licenseNumber)
    {
        var entity = await _dbContext.Renters
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.LicenseNumber == licenseNumber);

        return entity == null ? null : _mapper.Map<Renter>(entity);
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public override async Task<Renter?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Renters
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        return entity == null ? null : _mapper.Map<Renter>(entity);
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public override async Task<List<Renter>> GetAllAsync()
    {
        var entities = await _dbContext.Renters
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<Renter>>(entities);
    }

    /// <summary>
    /// Checks if an entity with the specified identifier exists
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if entity exists, otherwise false</returns>
    public override async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Renters.AnyAsync(r => r.Id == id);
    }
}