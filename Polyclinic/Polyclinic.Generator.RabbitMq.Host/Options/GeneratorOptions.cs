namespace Polyclinic.Generator.RabbitMq.Host.Options;

/// <summary>
/// Настройки генератора данных
/// Определяют границы значений для создаваемых сущностей
/// </summary>
public class GeneratorOptions
{
    /// <summary>
    /// Минимальный идентификатор пациента
    /// </summary>
    public int MinPatientId { get; set; } = 1;

    /// <summary>
    /// Максимальный идентификатор пациента
    /// </summary>
    public int MaxPatientId { get; set; } = 12;

    /// <summary>
    /// Минимальный идентификатор врача
    /// </summary>
    public int MinDoctorId { get; set; } = 1;

    /// <summary>
    /// Максимальный идентификатор врача
    /// </summary>
    public int MaxDoctorId { get; set; } = 10;

    /// <summary>
    /// Дата начала периода генерации записей
    /// </summary>
    public DateTime StartDate { get; set; } = new DateTime(2025, 2, 18, 12, 0, 0);

    /// <summary>
    /// Дата окончания периода генерации записей
    /// </summary>
    public DateTime EndDate { get; set; } = new DateTime(2025, 3, 18, 12, 0, 0);

    /// <summary>
    /// Минимальный номер кабинета
    /// </summary>
    public int MinRoomNumber { get; set; } = 100;

    /// <summary>
    /// Максимальный номер кабинета
    /// </summary>
    public int MaxRoomNumber { get; set; } = 599;

    /// <summary>
    /// Вероятность того, что прием является повторным (от 0.0 до 1.0)
    /// </summary>
    public float RepeatProbability { get; set; } = 0.3f;
}