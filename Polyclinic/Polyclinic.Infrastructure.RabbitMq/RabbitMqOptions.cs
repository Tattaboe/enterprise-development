namespace Polyclinic.Infrastructure.RabbitMq;

/// <summary>
/// Настройки конфигурации для подключения и работы с RabbitMQ
/// </summary>
public class RabbitMqOptions
{
    /// <summary>
    /// Имя очереди для прослушивания сообщений
    /// </summary>
    public required string QueueName { get; set; } = "appointment-contracts";

    /// <summary>
    /// Количество повторных попыток обработки сообщения при возникновении ошибок
    /// </summary>
    public int RetryCount { get; set; } = 5;

    /// <summary>
    /// Задержка между повторными попытками в миллисекундах
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;
}