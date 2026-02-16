namespace Polyclinic.Application.Contracts.Doctors;

/// <summary>
/// DTO врача для чтения
/// </summary>
/// <param name="Id">Уникальный идентификатор врача</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FullName">ФИО врача</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="SpecializationId">Идентификатор специализации</param>
/// <param name="ExperienceYears">Стаж работы (лет)</param>
public record DoctorDto(
    int Id,
    string PassportNumber,
    string FullName,
    DateTime BirthDate,
    int SpecializationId,
    int ExperienceYears
);