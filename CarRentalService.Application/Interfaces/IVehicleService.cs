using CarRentalService.Application.Contracts.Vehicle;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing vehicle operations
/// Provides methods for Create, Read, Update, and Delete (CRUD) operations on vehicles
/// </summary>
public interface IVehicleService
{
    /// <summary>
    /// Creates a new vehicle record in the system
    /// </summary>
    public Task<VehicleResponse> CreateAsync(VehicleRequest request);

    /// <summary>
    /// Retrieves a vehicle by its unique identifier
    /// </summary>
    public Task<VehicleResponse?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all vehicle records from the system
    /// </summary>
    public Task<List<VehicleResponse>> GetAllAsync();

    /// <summary>
    /// Updates an existing vehicle's information
    /// </summary>
    public Task<VehicleResponse?> UpdateAsync(Guid id, VehicleRequest request);

    /// <summary>
    /// Deletes a vehicle record from the system
    /// </summary>
    public Task<bool> DeleteAsync(Guid id);
}