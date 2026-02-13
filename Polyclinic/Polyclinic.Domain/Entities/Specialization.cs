namespace Polyclinic.Domain.Entities;

/// <summary>
/// Специализация врача (справочник)
/// </summary>
public class Specialization
{
    /// <summary>
    /// Уникальный идентификатор специализации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание специализации
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Код специализации
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Список врачей с этой специализацией
    /// </summary>
    public List<Doctor> Doctors { get; set; } = [];
}
