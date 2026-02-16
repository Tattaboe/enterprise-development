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
    DateTime AppointmentDateTime,
    string RoomNumber,
    bool IsRepeat,
    int PatientId,
    int DoctorId
);