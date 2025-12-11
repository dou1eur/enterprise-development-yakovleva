namespace CarRentalService.Application.Contracts.Common;

/// <summary>
/// Generic collection response DTO
/// </summary>
/// <typeparam name="T">Type of items in the collection</typeparam>
public sealed record CollectionResponse<T>(
    /// <summary>
    /// List of items
    /// </summary>
    List<T> Items
);