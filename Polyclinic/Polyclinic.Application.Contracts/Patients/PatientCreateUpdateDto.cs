using Polyclinic.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [MaxLength(20, ErrorMessage = "Номер паспорта не должен превышать 20 символов")]
    string PassportNumber,

    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(150, ErrorMessage = "ФИО не должно превышать 150 символов")]
    string FullName,

    [Required(ErrorMessage = "Пол обязателен")]
    Gender Gender,

    [Required(ErrorMessage = "Дата рождения обязательна")]
    [DataType(DataType.Date)]
    DateTime BirthDate,

    [Required(ErrorMessage = "Адрес обязателен")]
    [MaxLength(250, ErrorMessage = "Адрес не должен превышать 250 символов")]
    string Address,

    [Required(ErrorMessage = "Группа крови обязательна")]
    BloodGroup BloodGroup,

    [Required(ErrorMessage = "Резус-фактор обязателен")]
    RhFactor RhFactor,

    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    [MaxLength(20, ErrorMessage = "Телефон не должен превышать 20 символов")]
    string PhoneNumber
);