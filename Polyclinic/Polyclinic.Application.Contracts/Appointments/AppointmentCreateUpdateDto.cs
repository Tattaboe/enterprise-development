using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Application.Contracts.Appointments;

/// <summary>
/// DTO для создания и обновления записи на прием
/// </summary>
/// <param name="AppointmentDateTime">Дата и время приема</param>
/// <param name="RoomNumber">Номер кабинета</param>
/// <param name="IsRepeat">Признак повторного приема</param>
/// <param name="PatientId">Идентификатор пациента</param>
/// <param name="DoctorId">Идентификатор врача</param>
public record AppointmentCreateUpdateDto(
    [Required(ErrorMessage = "Дата и время приема обязательны")]
    DateTime AppointmentDateTime,

    [Required(ErrorMessage = "Номер кабинета обязателен")]
    [MaxLength(10, ErrorMessage = "Номер кабинета не должен превышать 10 символов")]
    string RoomNumber,

    bool IsRepeat,

    [Required(ErrorMessage = "Необходимо указать пациента")]
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный идентификатор пациента")]
    int PatientId,

    [Required(ErrorMessage = "Необходимо указать врача")]
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный идентификатор врача")]
    int DoctorId
);