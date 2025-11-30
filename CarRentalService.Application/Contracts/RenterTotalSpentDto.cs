namespace CarRentalService.Application.Contracts;

/// <summary>
/// DTO for renter total spent information
/// </summary>
public sealed record RenterTotalSpentDto(RenterDto Renter, decimal TotalSpent);