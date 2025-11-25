using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Base controller for CRUD operations
/// Provides common endpoints for create, read, update, and delete operations
/// </summary>
/// <typeparam name="TDto">The data transfer object type</typeparam>
/// <typeparam name="TCreateUpdateDto">The create/update data transfer object type</typeparam>
/// <typeparam name="TId">The identifier type</typeparam>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TId> : ControllerBase
{
    protected readonly ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TId>> _logger;

    /// <summary>
    /// Initializes a new instance of the CrudControllerBase class
    /// </summary>
    /// <param name="logger">The logger instance</param>
    protected CrudControllerBase(ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TId>> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual ActionResult<List<TDto>> GetAll()
        => ExecuteWithLogging(nameof(GetAll), () => Ok(GetService().GetAll()));

    /// <summary>
    /// Retrieves an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual ActionResult<TDto> Get(TId id)
        => ExecuteWithLogging(nameof(Get), () =>
        {
            try
            {
                var result = GetService().Get(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });

    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="dto">The entity data</param>
    /// <returns>The created entity</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual ActionResult<TDto> Create(TCreateUpdateDto dto)
        => ExecuteWithLogging(nameof(Create), () =>
        {
            try
            {
                var result = GetService().Create(dto);
                return CreatedAtAction(nameof(Get), new { id = GetId(result) }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        });

    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="dto">The updated entity data</param>
    /// <param name="id">The entity identifier</param>
    /// <returns>The updated entity</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual ActionResult<TDto> Update(TCreateUpdateDto dto, TId id)
        => ExecuteWithLogging(nameof(Update), () =>
        {
            try
            {
                var result = GetService().Update(dto, id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        });

    /// <summary>
    /// Deletes an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual ActionResult Delete(TId id)
        => ExecuteWithLogging(nameof(Delete), () =>
        {
            try
            {
                var result = GetService().Delete(id);
                if (result)
                    return NoContent();
                else
                    return NotFound();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });

    /// <summary>
    /// Extracts the identifier from a DTO
    /// </summary>
    /// <param name="dto">The data transfer object</param>
    /// <returns>The identifier</returns>
    protected abstract object GetId(TDto dto);

    /// <summary>
    /// Gets the service instance for CRUD operations
    /// </summary>
    /// <returns>The service instance</returns>
    protected abstract dynamic GetService();

    /// <summary>
    /// Executes an action with logging and error handling
    /// </summary>
    /// <param name="operationName">The name of the operation</param>
    /// <param name="action">The action to execute</param>
    /// <returns>The action result</returns>
    protected ActionResult ExecuteWithLogging(string operationName, Func<ActionResult> action)
    {
        try
        {
            _logger.LogInformation("Executing {Operation}", operationName);
            return action();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing {Operation}", operationName);
            return StatusCode(500, "Internal server error");
        }
    }
}