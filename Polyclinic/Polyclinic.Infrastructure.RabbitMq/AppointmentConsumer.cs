using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Polyclinic.Infrastructure.RabbitMq;

/// <summary>
/// Фоновый сервис для чтения сообщений из RabbitMQ и создания записей на прием
/// </summary>
/// <param name="connection">Активное соединение с RabbitMQ</param>
/// <param name="options">Настройки очереди и повторных попыток</param>
/// <param name="logger">Логгер для записи событий и ошибок</param>
/// <param name="serviceScopeFactory">Фабрика областей видимости для получения Scoped-сервисов</param>
public class AppointmentConsumer(
    IConnection connection,
    IOptions<RabbitMqOptions> options,
    ILogger<AppointmentConsumer> logger,
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly RabbitMqOptions _options = options.Value;
    private IChannel? _channel;

    /// <summary>
    /// Основной цикл выполнения фоновой задачи
    /// Инициализирует канал, объявляет очередь и подписывается на события получения сообщений
    /// </summary>
    /// <param name="stoppingToken">Токен отмены для остановки сервиса</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Инициализация подключения к RabbitMQ.");

            _channel = await connection.CreateChannelAsync(options: null, cancellationToken: stoppingToken);

            await _channel.QueueDeclareAsync(
                queue: _options.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 10, global: false, cancellationToken: stoppingToken);

            logger.LogInformation("Подключение успешно. Очередь: {QueueName}", _options.QueueName);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (sender, @event) =>
            {
                var content = Encoding.UTF8.GetString(@event.Body.Span);
                logger.LogInformation("Получено сообщение: {Content}", content);

                try
                {
                    var dto = JsonSerializer.Deserialize<AppointmentCreateUpdateDto>(content);

                    if (dto is null)
                    {
                        logger.LogWarning("Сообщение пустое или некорректный JSON. Удаляем из очереди.");
                        await _channel.BasicAckAsync(@event.DeliveryTag, multiple: false);
                        return;
                    }

                    var processed = await ProcessMessageAsync(dto, stoppingToken);

                    if (processed)
                    {
                        await _channel.BasicAckAsync(@event.DeliveryTag, multiple: false);
                        logger.LogInformation("Сообщение успешно обработано.");
                    }
                    else
                    {
                        logger.LogError("Сообщение не обработано после {RetryCount} попыток. Удаляем.", _options.RetryCount);
                        await _channel.BasicNackAsync(@event.DeliveryTag, multiple: false, requeue: false);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Критическая ошибка в обработчике событий RabbitMQ.");
                    await _channel.BasicNackAsync(@event.DeliveryTag, multiple: false, requeue: false);
                }
            };

            await _channel.BasicConsumeAsync(queue: _options.QueueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Операция получения сообщений была отменена, RabbitMQ Consumer останавливается.");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Ошибка запуска RabbitMQ Consumer.");
        }
    }

    /// <summary>
    /// Обрабатывает полученное DTO сообщения, пытаясь сохранить запись в БД
    /// </summary>
    /// <param name="dto">Данные для создания записи на прием</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Возвращает true, если сообщение обработано успешно или отброшено из-за логической ошибки; false, если исчерпаны лимиты попыток</returns>
    private async Task<bool> ProcessMessageAsync(AppointmentCreateUpdateDto dto, CancellationToken token)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var appointmentService = scope.ServiceProvider.GetRequiredService<IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>>();

        var currentRetry = 0;

        while (currentRetry <= _options.RetryCount)
        {
            if (token.IsCancellationRequested) return false;

            try
            {
                await appointmentService.Create(dto);
                return true;
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError(ex, "Ошибка целостности данных: {Message}. Сообщение будет пропущено.", ex.Message);
                return true;
            }
            catch (Exception ex)
            {
                currentRetry++;
                logger.LogWarning(ex, "Ошибка при создании записи (Попытка {Retry}/{Max}). Ждем {Delay}мс...",
                    currentRetry, _options.RetryCount, _options.RetryDelayMs);

                if (currentRetry > _options.RetryCount)
                {
                    logger.LogError("Превышен лимит попыток для сообщения.");
                    return false;
                }

                await Task.Delay(_options.RetryDelayMs, token);
            }
        }

        return false;
    }

    /// <summary>
    /// Освобождает ресурсы канала
    /// </summary>
    public override void Dispose()
    {
        _channel?.Dispose();
        GC.SuppressFinalize(this);
    }
}