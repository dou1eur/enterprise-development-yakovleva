using CarRentalService.Application.Contracts.ModelGeneration;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing model generation operations
/// Provides methods for Create, Read, Update, and Delete (CRUD) operations on vehicle model generations
/// </summary>
public interface IModelGenerationService
{
    /// <summary>
    /// Creates a new model generation record
    /// </summary>
    public Task<ModelGenerationResponse> CreateAsync(ModelGenerationRequest request);

    /// <summary>
    /// Retrieves a model generation by unique identifier
    /// </summary>
    public Task<ModelGenerationResponse?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all model generation records
    /// </summary>
    public Task<List<ModelGenerationResponse>> GetAllAsync();

    /// <summary>
    /// Updates an existing model generation record
    /// </summary>
    public Task<ModelGenerationResponse?> UpdateAsync(Guid id, ModelGenerationRequest request);

    /// <summary>
    /// Deletes a model generation record by identifier
    /// </summary>
    public Task<bool> DeleteAsync(Guid id);
}