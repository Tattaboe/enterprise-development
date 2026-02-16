using Polyclinic.Domain.Enums;

namespace Polyclinic.Application.Contracts.Patients;

/// <summary>
/// DTO для создания и обновления пациента
/// </summary>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FullName">ФИО пациента</param>
/// <param name="Gender">Пол пациента</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="Address">Адрес проживания</param>
/// <param name="BloodGroup">Группа крови</param>
/// <param name="RhFactor">Резус-фактор</param>
/// <param name="PhoneNumber">Контактный телефон</param>
public record PatientCreateUpdateDto(
    string PassportNumber,
    string FullName,
    Gender Gender,
    DateTime BirthDate,
    string Address,
    BloodGroup BloodGroup,
    RhFactor RhFactor,
    string PhoneNumber
);