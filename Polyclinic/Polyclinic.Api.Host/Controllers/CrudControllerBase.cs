using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Contracts;

namespace Polyclinic.Api.Host.Controllers;

/// <summary>
/// Базовый абстрактный контроллер, реализующий стандартные CRUD-операции
/// </summary>
/// <typeparam name="TDto">Тип DTO для чтения</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания и обновления</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> service)
    : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Получает список всех сущностей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            var result = await service.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Get(TKey id)
    {
        try
        {
            var result = await service.Get(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    /// <summary>
    /// Создает новую сущность
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto dto)
    {
        try
        {
            var result = await service.Create(dto);
            return CreatedAtAction(nameof(this.Create), result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="dto">DTO с новыми данными</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Update(TKey id, [FromBody] TCreateUpdateDto dto)
    {
        try
        {
            var result = await service.Update(dto, id);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult> Delete(TKey id)
    {
        try
        {
            await service.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }
}