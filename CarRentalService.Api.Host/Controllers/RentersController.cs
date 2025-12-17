using CarRentalService.Application.Contracts.Renter;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing renters (customers)
/// Provides CRUD operations for renter entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentersController(
    ILogger<RentersController> logger,
    IRenterService renterService)
    : CrudControllerBase<RenterResponse, RenterRequest, Guid>(logger)
{
    /// <summary>
    /// Extracts the identifier from a RenterResponse DTO
    /// </summary>
    /// <param name="dto">The renter response DTO</param>
    /// <returns>The renter identifier</returns>
    protected override object GetId(RenterResponse dto) => dto.Id;

    /// <summary>
    /// Gets the service instance for renter CRUD operations
    /// </summary>
    /// <returns>The renter service instance</returns>
    protected override dynamic GetService() => renterService;
}