using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Collection response DTO for model generations
/// </summary>
public class ModelGenerationCollectionResponse
{
    /// <summary>
    /// List of model generations
    /// </summary>
    public List<ModelGenerationDto> ModelGenerations { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of ModelGenerationCollectionResponse
    /// </summary>
    public ModelGenerationCollectionResponse() { }

    /// <summary>
    /// Initializes a new instance of ModelGenerationCollectionResponse with specified model generations
    /// </summary>
    /// <param name="modelGenerations">List of model generation DTOs</param>
    public ModelGenerationCollectionResponse(List<ModelGenerationDto> modelGenerations)
    {
        ModelGenerations = modelGenerations;
    }
}
