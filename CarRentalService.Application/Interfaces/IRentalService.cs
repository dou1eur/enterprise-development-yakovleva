using CarRentalService.Application.Contracts.Rental;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing rental operations
/// Provides methods for Create, Read, Update, and Delete (CRUD) operations on rentals
/// </summary>
public interface IRentalService
{
    /// <summary>
    /// Creates a new rental record in the system
    /// </summary>
    public Task<RentalResponse> CreateAsync(RentalRequest request);

    /// <summary>
    /// Retrieves a rental by its unique identifier
    /// </summary>
    public Task<RentalResponse?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all rental records from the system
    /// </summary>
    public Task<List<RentalResponse>> GetAllAsync();

    /// <summary>
    /// Updates an existing rental's information
    /// </summary>
    public Task<RentalResponse?> UpdateAsync(Guid id, RentalRequest request);

    /// <summary>
    /// Deletes a rental record from the system
    /// </summary>
    public Task<bool> DeleteAsync(Guid id);
}