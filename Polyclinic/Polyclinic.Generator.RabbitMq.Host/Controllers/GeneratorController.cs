using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Generator.RabbitMq.Host.Services;

namespace Polyclinic.Generator.RabbitMq.Host.Controllers;

/// <summary>
/// Контроллер для управления генерацией тестовых данных
/// </summary>
/// <param name="generator">Сервис генерации фейковых данных</param>
/// <param name="producer">Сервис публикации сообщений в RabbitMQ</param>
/// <param name="logger">Логгер</param>
[Route("api/[controller]")]
[ApiController]
public class GeneratorController(
    AppointmentGenerator generator,
    AppointmentProducer producer,
    ILogger<GeneratorController> logger) : ControllerBase
{
    /// <summary>
    /// Инициирует генерацию указанного количества записей на прием и отправляет их в очередь RabbitMQ
    /// </summary>
    /// <param name="count">Количество записей для генерации (по умолчанию 10)</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список сгенерированных и отправленных записей</returns>
    [HttpPost("generate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<AppointmentCreateUpdateDto>>> Generate(
        [FromQuery] int count = 10,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запуск генерации {Count} записей на прием...", count);

        try
        {
            var appointments = generator.Generate(count).ToList();

            foreach (var appointment in appointments)
            {
                await producer.PublishAsync(appointment, cancellationToken);
            }

            logger.LogInformation("Успешно сгенерировано и отправлено {Count} записей.", appointments.Count);

            return Ok(appointments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при генерации или отправке данных в RabbitMQ.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }
}