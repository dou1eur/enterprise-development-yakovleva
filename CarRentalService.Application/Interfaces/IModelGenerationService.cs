using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing model generation operations
/// Provides methods for basic CRUD operations on model generations
/// </summary>
public interface IModelGenerationService
{
    /// <summary>
    /// Creates a new model generation record
    /// </summary>
    /// <param name="request">Data transfer object containing model generation creation details</param>
    /// <returns>The created model generation data transfer object</returns>
    public Task<ModelGenerationDto> CreateAsync(CreateModelGenerationRequest request);

    /// <summary>
    /// Retrieves a model generation by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model generation</param>
    /// <returns>The model generation data transfer object if found; otherwise, null</returns>
    public Task<ModelGenerationDto?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all model generation records
    /// </summary>
    /// <returns>List of all model generation data transfer objects</returns>
    public Task<List<ModelGenerationDto>> GetAllAsync();

    /// <summary>
    /// Updates an existing model generation record
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to update</param>
    /// <param name="request">Data transfer object containing updated model generation details</param>
    /// <returns>The updated model generation data transfer object if successful; otherwise, null</returns>
    public Task<ModelGenerationDto?> UpdateAsync(Guid id, UpdateModelGenerationRequest request);

    /// <summary>
    /// Deletes a model generation record by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public Task<bool> DeleteAsync(Guid id);
}