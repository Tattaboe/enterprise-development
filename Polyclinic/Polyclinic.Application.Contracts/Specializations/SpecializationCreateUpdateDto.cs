namespace Polyclinic.Application.Contracts.Specializations;

/// <summary>
/// DTO для создания и обновления специализации
/// </summary>
/// <param name="Name">Название специализации</param>
/// <param name="Description">Описание специализации</param>
/// <param name="Code">Код специализации</param>
public record SpecializationCreateUpdateDto(
    string Name,
    string Description,
    string Code
);