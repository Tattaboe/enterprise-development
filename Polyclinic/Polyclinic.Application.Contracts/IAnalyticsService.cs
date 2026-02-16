using Polyclinic.Application.Contracts.Analytics;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;

namespace Polyclinic.Application.Contracts;

/// <summary>
/// Сервис для получения аналитических выборок и отчетов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает список врачей со стажем работы более указанного количества лет
    /// </summary>
    /// <param name="minExperienceYears">Минимальный стаж в годах (по умолчанию 10)</param>
    /// <returns>
    /// Список врачей, удовлетворяющих условию
    /// </returns>
    public Task<IList<DoctorDto>> GetDoctorsWithExperienceAsync(int minExperienceYears = 10);

    /// <summary>
    /// Возвращает список пациентов, записанных к конкретному врачу, отсортированный по ФИО
    /// </summary>
    /// <param name="doctorId">Идентификатор врача</param>
    /// <returns>
    /// Отсортированный список пациентов
    /// </returns>
    public Task<IList<PatientDto>> GetPatientsByDoctorAsync(int doctorId);

    /// <summary>
    /// Возвращает статистику по повторным приемам за указанный месяц
    /// </summary>
    /// <param name="targetDate">Дата, определяющая месяц и год выборки</param>
    /// <returns>
    /// Статистика по приемам
    /// </returns>
    public Task<MonthlyAppointmentStatsDto> GetMonthlyRepeatStatsAsync(DateTime targetDate);

    /// <summary>
    /// Возвращает пациентов старше определенного возраста, посетивших более одного врача, отсортированных по дате рождения
    /// </summary>
    /// <param name="minAge">Минимальный возраст пациента (по умолчанию 30)</param>
    /// <returns>
    /// Список пациентов
    /// </returns>
    public Task<IList<PatientDto>> GetPatientsWithMultipleDoctorsAsync(int minAge = 30);

    /// <summary>
    /// Возвращает список приемов в указанном кабинете за текущий месяц (относительно переданной даты)
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="targetDate">Дата для определения месяца выборки</param>
    /// <returns>
    /// Список приемов
    /// </returns>
    public Task<IList<AppointmentDto>> GetAppointmentsByRoomAsync(string roomNumber, DateTime targetDate);
}