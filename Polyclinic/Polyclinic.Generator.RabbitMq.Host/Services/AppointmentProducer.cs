using Microsoft.Extensions.Options;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Generator.RabbitMq.Host.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Polyclinic.Generator.RabbitMq.Host.Services;

/// <summary>
/// Сервис для публикации сообщений о создании записей на прием в RabbitMQ
/// </summary>
/// <param name="connection">Активное соединение с RabbitMQ</param>
/// <param name="options">Настройки очереди и повторных попыток</param>
/// <param name="logger">Логгер для записи событий и ошибок</param>
public class AppointmentProducer(
    IConnection connection,
    IOptions<RabbitMqOptions> options,
    ILogger<AppointmentProducer> logger) : IDisposable
{
    private readonly RabbitMqOptions _options = options.Value;
    private IChannel? _channel;

    /// <summary>
    /// Публикует сообщение с данными о записи на прием в очередь
    /// </summary>
    /// <param name="appointment">DTO с данными записи</param>
    /// <param name="token">Токен отмены</param>
    public async Task PublishAsync(AppointmentCreateUpdateDto appointment, CancellationToken token = default)
    {
        await EnsureChannelAsync(token);

        var json = JsonSerializer.Serialize(appointment);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent,
            ContentType = "application/json"
        };

        await _channel!.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _options.QueueName,
            mandatory: false,
            basicProperties: properties,
            body: body,
            cancellationToken: token);

        logger.LogInformation("Отправлено сообщение в очередь {Queue}: {Json}.", _options.QueueName, json);
    }

    /// <summary>
    /// Инициализирует канал и объявляет очередь, если это еще не было сделано
    /// </summary>
    private async Task EnsureChannelAsync(CancellationToken token)
    {
        if (_channel is not null) return;

        logger.LogInformation("Создание RabbitMQ канала для очереди {QueueName}.", _options.QueueName);

        _channel = await connection.CreateChannelAsync(cancellationToken: token);

        await _channel.QueueDeclareAsync(
            queue: _options.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: token);

        logger.LogInformation("Канал RabbitMQ успешно создан.");
    }

    /// <summary>
    /// Освобождает ресурсы канала
    /// </summary>
    public void Dispose()
    {
        _channel?.Dispose();
        GC.SuppressFinalize(this);
    }
}