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
    string PassportNumber,
    string FullName,
    DateTime BirthDate,
    int SpecializationId,
    int ExperienceYears
);