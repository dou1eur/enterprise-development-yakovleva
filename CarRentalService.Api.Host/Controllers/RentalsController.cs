using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing rental operations
/// Provides endpoints for rental CRUD operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentalsController : CrudControllerBase<RentalDto, CreateRentalRequest, UpdateRentalRequest, Guid>
{
    private readonly IRentalService _rentalService;

    /// <summary>
    /// Initializes a new instance of the RentalsController class
    /// </summary>
    /// <param name="rentalService">The rental service</param>
    /// <param name="logger">The logger instance</param>
    public RentalsController(IRentalService rentalService, ILogger<RentalsController> logger)
        : base(logger)
    {
        _rentalService = rentalService;
    }

    /// <summary>
    /// Extracts the identifier from a rental DTO
    /// </summary>
    /// <param name="dto">The rental data transfer object</param>
    /// <returns>The rental identifier</returns>
    protected override object GetId(RentalDto dto) => dto.Id;

    /// <summary>
    /// Gets the rental service instance
    /// </summary>
    /// <returns>The rental service instance</returns>
    protected override dynamic GetService() => _rentalService;
}