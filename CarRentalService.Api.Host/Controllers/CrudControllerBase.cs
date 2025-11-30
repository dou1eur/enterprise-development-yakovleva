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
/// <typeparam name="TCreateDto">The create data transfer object type</typeparam>
/// <typeparam name="TUpdateDto">The update data transfer object type</typeparam>
/// <typeparam name="TId">The identifier type</typeparam>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateDto, TUpdateDto, TId> : ControllerBase
    where TId : struct
{
    protected readonly ILogger<CrudControllerBase<TDto, TCreateDto, TUpdateDto, TId>> _logger;

    /// <summary>
    /// Initializes a new instance of the CrudControllerBase class
    /// </summary>
    /// <param name="logger">The logger instance</param>
    protected CrudControllerBase(ILogger<CrudControllerBase<TDto, TCreateDto, TUpdateDto, TId>> logger)
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
    public virtual async Task<ActionResult<List<TDto>>> GetAll()
        => await ExecuteWithLoggingAsync(nameof(GetAll), async () =>
        {
            var result = await GetService().GetAllAsync();
            return Ok(result);
        });

    /// <summary>
    /// Retrieves an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Get(TId id)
        => await ExecuteWithLoggingAsync(nameof(Get), async () =>
        {
            var result = await GetService().GetAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        });

    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="request">The entity creation data</param>
    /// <returns>The created entity</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Create(TCreateDto request)
        => await ExecuteWithLoggingAsync(nameof(Create), async () =>
        {
            try
            {
                var result = await GetService().CreateAsync(request);
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
    /// <param name="id">The entity identifier</param>
    /// <param name="request">The updated entity data</param>
    /// <returns>The updated entity</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Update(TId id, TUpdateDto request)
        => await ExecuteWithLoggingAsync(nameof(Update), async () =>
        {
            try
            {
                var result = await GetService().UpdateAsync(id, request);
                if (result == null)
                    return NotFound();
                return Ok(result);
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
    public virtual async Task<ActionResult> Delete(TId id)
        => await ExecuteWithLoggingAsync(nameof(Delete), async () =>
        {
            var result = await GetService().DeleteAsync(id);
            if (result)
                return NoContent();
            else
                return NotFound();
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
    protected async Task<ActionResult> ExecuteWithLoggingAsync(string operationName, Func<Task<ActionResult>> action)
    {
        try
        {
            _logger.LogInformation("Executing {Operation}", operationName);
            return await action();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing {Operation}", operationName);
            return StatusCode(500, "Internal server error");
        }
    }
}