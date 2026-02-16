using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Analytics;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для получения аналитических данных и отчетов
/// </summary>
public class AnalyticsService(
    IRepository<Doctor, int> doctorRepository,
    IRepository<Appointment, int> appointmentRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IList<DoctorDto>> GetDoctorsWithExperienceAsync(int minExperienceYears = 10)
    {
        var doctors = await doctorRepository.ReadAll();

        var filteredDoctors = doctors
            .Where(d => d.ExperienceYears >= minExperienceYears)
            .ToList();

        return mapper.Map<IList<DoctorDto>>(filteredDoctors);
    }

    /// <inheritdoc />
    public async Task<IList<PatientDto>> GetPatientsByDoctorAsync(int doctorId)
    {
        var appointments = await appointmentRepository.ReadAll();

        var patients = appointments
            .Where(a => a.DoctorId == doctorId && a.Patient != null)
            .Select(a => a.Patient!)
            .DistinctBy(p => p.Id)
            .OrderBy(p => p.FullName)
            .ToList();

        return mapper.Map<IList<PatientDto>>(patients);
    }

    /// <inheritdoc />
    public async Task<MonthlyAppointmentStatsDto> GetMonthlyRepeatStatsAsync(DateTime targetDate)
    {
        var appointments = await appointmentRepository.ReadAll();

        var monthlyAppointments = appointments
            .Where(a => a.AppointmentDateTime.Year == targetDate.Year
                     && a.AppointmentDateTime.Month == targetDate.Month)
            .ToList();

        var totalCount = monthlyAppointments.Count;
        var repeatCount = monthlyAppointments.Count(a => a.IsRepeat);

        return new MonthlyAppointmentStatsDto(
            Year: targetDate.Year,
            Month: targetDate.Month,
            RepeatAppointmentCount: repeatCount,
            TotalAppointmentCount: totalCount
        );
    }

    /// <inheritdoc />
    public async Task<IList<PatientDto>> GetPatientsWithMultipleDoctorsAsync(int minAge = 30)
    {
        var appointments = await appointmentRepository.ReadAll();
        var today = DateTime.Today;

        var resultPatients = appointments
            .Where(a => a.Patient != null)
            .GroupBy(a => a.Patient)
            .Where(g =>
            {
                var patient = g.Key!;
                var isOldEnough = patient.GetAge(today) > minAge;

                var hasMultipleDoctors = g.Select(a => a.DoctorId).Distinct().Count() > 1;

                return isOldEnough && hasMultipleDoctors;
            })
            .Select(g => g.Key!)
            .OrderBy(p => p.BirthDate)
            .ToList();

        return mapper.Map<IList<PatientDto>>(resultPatients);
    }

    /// <inheritdoc />
    public async Task<IList<AppointmentDto>> GetAppointmentsByRoomAsync(string roomNumber, DateTime targetDate)
    {
        var appointments = await appointmentRepository.ReadAll();

        var filtered = appointments
            .Where(a => a.RoomNumber == roomNumber
                     && a.AppointmentDateTime.Year == targetDate.Year
                     && a.AppointmentDateTime.Month == targetDate.Month)
            .ToList();

        return mapper.Map<IList<AppointmentDto>>(filtered);
    }
}