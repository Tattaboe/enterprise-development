namespace Polyclinic.Application.Contracts.Specializations;

/// <summary>
/// DTO специализации для чтения
/// </summary>
/// <param name="Id">Уникальный идентификатор специализации</param>
/// <param name="Name">Название специализации</param>
/// <param name="Description">Описание специализации</param>
/// <param name="Code">Код специализации</param>
public record SpecializationDto(
    int Id,
    string Name,
    string Description,
    string Code
);