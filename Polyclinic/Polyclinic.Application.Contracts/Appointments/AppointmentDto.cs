namespace Polyclinic.Application.Contracts.Appointments;

/// <summary>
/// DTO записи на прием для чтения
/// </summary>
/// <param name="Id">Уникальный идентификатор записи</param>
/// <param name="AppointmentDateTime">Дата и время приема</param>
/// <param name="RoomNumber">Номер кабинета</param>
/// <param name="IsRepeat">Признак повторного приема</param>
/// <param name="PatientId">Идентификатор пациента</param>
/// <param name="DoctorId">Идентификатор врача</param>
public record AppointmentDto(
    int Id,
    DateTime AppointmentDateTime,
    string RoomNumber,
    bool IsRepeat,
    int PatientId,
    int DoctorId
);