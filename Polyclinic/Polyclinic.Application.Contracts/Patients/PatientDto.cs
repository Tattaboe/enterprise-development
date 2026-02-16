using Polyclinic.Domain.Enums;

namespace Polyclinic.Application.Contracts.Patients;

/// <summary>
/// DTO пациента для чтения
/// </summary>
/// <param name="Id">Уникальный идентификатор пациента</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FullName">ФИО пациента</param>
/// <param name="Gender">Пол пациента</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="Address">Адрес проживания</param>
/// <param name="BloodGroup">Группа крови</param>
/// <param name="RhFactor">Резус-фактор</param>
/// <param name="PhoneNumber">Контактный телефон</param>
public record PatientDto(
    int Id,
    string PassportNumber,
    string FullName,
    Gender Gender,
    DateTime BirthDate,
    string Address,
    BloodGroup BloodGroup,
    RhFactor RhFactor,
    string PhoneNumber
);