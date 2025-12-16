using Microsoft.AspNetCore.Mvc;

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
public abstract class CrudControllerBase<TDto, TCreateDto, TUpdateDto, TId>(
    ILogger<CrudControllerBase<TDto, TCreateDto, TUpdateDto, TId>> logger) : ControllerBase
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
    where TId : struct
{
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
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateDto request)
        => await ExecuteWithLoggingAndValidationAsync<TCreateDto>(nameof(Create), async () =>
        {
            try
            {
                var result = await GetService().CreateAsync(request);
                return CreatedAtAction(nameof(Get), new { id = GetId(result) }, result);
            }
            catch (ArgumentException ex)
            {
                logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Create));
                return BadRequest("Invalid request data");
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
    public virtual async Task<ActionResult<TDto>> Update(TId id, [FromBody] TUpdateDto request)
        => await ExecuteWithLoggingAndValidationAsync<TUpdateDto>(nameof(Update), async () =>
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
                logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Update));
                return BadRequest("Invalid request data");
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
            logger.LogInformation("Executing {Operation}", operationName);
            return await action();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing {Operation}", operationName);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Executes an action with logging, validation and error handling
    /// </summary>
    /// <param name="operationName">The name of the operation</param>
    /// <param name="request">The request object to validate</param>
    /// <param name="action">The action to execute</param>
    /// <returns>The action result</returns>
    protected async Task<ActionResult> ExecuteWithLoggingAndValidationAsync<TRequest>(string operationName, Func<Task<ActionResult>> action)
        where TRequest : class
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                    operationName, ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest("Invalid request data");
            }

            logger.LogInformation("Executing {Operation}", operationName);
            return await action();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing {Operation}", operationName);
            return StatusCode(500, "Internal server error");
        }
    }
}