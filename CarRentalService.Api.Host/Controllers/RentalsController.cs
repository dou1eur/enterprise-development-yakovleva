using CarRentalService.Application.Contracts.Rental;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing rentals
/// Provides CRUD operations for rental entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentalsController(
    ILogger<RentalsController> logger,
    IRentalService rentalService)
    : CrudControllerBase<RentalResponse, RentalRequest, RentalRequest, Guid>(logger)
{
    /// <summary>
    /// Extracts the identifier from a RentalResponse DTO
    /// </summary>
    /// <param name="dto">The rental response DTO</param>
    /// <returns>The rental identifier</returns>
    protected override object GetId(RentalResponse dto) => dto.Id;

    /// <summary>
    /// Gets the service instance for rental CRUD operations
    /// </summary>
    /// <returns>The rental service instance</returns>
    protected override dynamic GetService() => rentalService;
}