using CarRentalService.Application.Contracts.VehicleModel;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing vehicle model operations
/// Provides methods for Create, Read, Update, and Delete (CRUD) operations on vehicle models
/// </summary>
public interface IVehicleModelService
{
    /// <summary>
    /// Creates a new vehicle model record in the system
    /// </summary>
    public Task<VehicleModelResponse> CreateAsync(VehicleModelRequest request);

    /// <summary>
    /// Retrieves a vehicle model by its unique identifier
    /// </summary>
    public Task<VehicleModelResponse?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all vehicle model records from the system
    /// </summary>
    public Task<List<VehicleModelResponse>> GetAllAsync();

    /// <summary>
    /// Updates an existing vehicle model's information
    /// </summary>
    public Task<VehicleModelResponse?> UpdateAsync(Guid id, VehicleModelRequest request);

    /// <summary>
    /// Deletes a vehicle model record from the system
    /// </summary>
    public Task<bool> DeleteAsync(Guid id);
}