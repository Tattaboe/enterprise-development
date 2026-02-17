using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Application.Contracts.Specializations;

/// <summary>
/// DTO для создания и обновления специализации
/// </summary>
/// <param name="Name">Название специализации</param>
/// <param name="Description">Описание специализации</param>
/// <param name="Code">Код специализации</param>
public record SpecializationCreateUpdateDto(
    [Required(ErrorMessage = "Название специализации обязательно")]
    [MaxLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
    string Name,

    [MaxLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
    string Description,

    [Required(ErrorMessage = "Код специализации обязателен")]
    [MaxLength(20, ErrorMessage = "Код не должен превышать 20 символов")]
    string Code
);