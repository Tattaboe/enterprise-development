using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Application.Contracts.Doctors;

/// <summary>
/// DTO для создания и обновления врача
/// </summary>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FullName">ФИО врача</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="SpecializationId">Идентификатор специализации</param>
/// <param name="ExperienceYears">Стаж работы (лет)</param>
public record DoctorCreateUpdateDto(
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [MaxLength(20, ErrorMessage = "Номер паспорта не должен превышать 20 символов")]
    string PassportNumber,

    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(150, ErrorMessage = "ФИО не должно превышать 150 символов")]
    string FullName,

    [Required(ErrorMessage = "Дата рождения обязательна")]
    [DataType(DataType.Date)]
    DateTime BirthDate,

    [Required(ErrorMessage = "Необходимо указать специализацию")]
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный идентификатор специализации")]
    int SpecializationId,

    [Range(0, 100, ErrorMessage = "Стаж работы должен быть от 0 до 100 лет")]
    int ExperienceYears
);