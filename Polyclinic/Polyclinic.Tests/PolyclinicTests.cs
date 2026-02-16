using Polyclinic.Domain;

namespace Polyclinic.Tests;

/// <summary>
/// Тесты для поликлиники с использованием фикстуры
/// </summary>
public class PolyclinicTests(PolyclinicFixture fixture) : IClassFixture<PolyclinicFixture>
{
    /// <summary>
    /// ТЕСТ 1: Вывести информацию о всех врачах, стаж работы которых не менее 10 лет
    /// </summary>
    [Fact]
    public void GetDoctorsWithExperienceMoreThan10Years()
    {
        var actual = (
            from doctor in fixture.Doctors
            where doctor.ExperienceYears >= TestConstants.MinExperienceYears
            orderby doctor.FullName
            select doctor
        ).ToList();

        Assert.NotEmpty(actual);
        Assert.All(actual, doctor =>
            Assert.True(doctor.ExperienceYears >= TestConstants.MinExperienceYears));

        var excludedDoctors = fixture.Doctors.Except(actual);
        Assert.All(excludedDoctors, doctor =>
            Assert.True(doctor.ExperienceYears < TestConstants.MinExperienceYears));

        Assert.Equal([.. actual.OrderBy(d => d.FullName)], actual);
    }

    /// <summary>
    /// ТЕСТ 2: Вывести информацию о всех пациентах, записанных к указанному врачу, упорядочить по ФИО
    /// </summary>
    [Fact]
    public void GetPatientsByDoctorOrderedByFullName()
    {
        var testDoctor = fixture.Doctors.First(d => fixture.Appointments.Any(a => a.DoctorId == d.Id));

        var actual = (
            from appointment in fixture.Appointments
            where appointment.DoctorId == testDoctor.Id
            join patient in fixture.Patients on appointment.PatientId equals patient.Id
            orderby patient.FullName
            select patient
        ).Distinct().ToList();

        Assert.NotEmpty(actual);
        Assert.All(actual, patient =>
        {
            var hasAppointmentWithDoctor = fixture.Appointments
                .Any(a => a.PatientId == patient.Id && a.DoctorId == testDoctor.Id);
            Assert.True(hasAppointmentWithDoctor);
        });

        Assert.Equal([.. actual.OrderBy(p => p.FullName)], actual);
    }

    /// <summary>
    /// ТЕСТ 3: Вывести информацию о количестве повторных приемов пациентов за последний месяц
    /// </summary>
    [Fact]
    public void CountRepeatAppointmentsLastMonth()
    {
        var actual = (
            from appointment in fixture.Appointments
            where appointment.AppointmentDateTime >= TestConstants.StartOfLastMonth
            where appointment.AppointmentDateTime < TestConstants.StartOfCurrentMonth
            where appointment.IsRepeat
            select appointment
        ).ToList();

        Assert.All(actual, appointment =>
        {
            Assert.True(appointment.IsRepeat);
            Assert.True(appointment.AppointmentDateTime >= TestConstants.StartOfLastMonth);
            Assert.True(appointment.AppointmentDateTime < TestConstants.StartOfCurrentMonth);
        });

        var nonRepeatInLastMonth = fixture.Appointments
            .Where(a => a.AppointmentDateTime >= TestConstants.StartOfLastMonth)
            .Where(a => a.AppointmentDateTime < TestConstants.StartOfCurrentMonth)
            .Where(a => !a.IsRepeat);

        Assert.All(nonRepeatInLastMonth, a => Assert.False(a.IsRepeat));
    }

    /// <summary>
    /// ТЕСТ 4: Вывести информацию о пациентах старше 30 лет, 
    /// которые записаны на прием к нескольким врачам, упорядочить по дате рождения
    /// </summary>
    [Fact]
    public void GetPatientsOver30WithMultipleDoctorsOrderedByBirthDate()
    {
        var actual = (
            from patient in fixture.Patients
            where patient.GetAge(TestConstants.Today) >= TestConstants.MinPatientAge
            let doctorsCount = (
                from appointment in fixture.Appointments
                where appointment.PatientId == patient.Id
                select appointment.DoctorId
            ).Distinct().Count()
            where doctorsCount >= 2
            orderby patient.BirthDate
            select patient
        ).ToList();

        Assert.All(actual, patient =>
        {
            Assert.True(patient.GetAge(TestConstants.Today) >= TestConstants.MinPatientAge);

            var doctorsCount = fixture.Appointments
                .Where(a => a.PatientId == patient.Id)
                .Select(a => a.DoctorId)
                .Distinct()
                .Count();
            Assert.True(doctorsCount >= 2);
        });

        var excludedPatients = fixture.Patients.Except(actual);
        Assert.All(excludedPatients, patient =>
        {
            if (patient.GetAge(TestConstants.Today) >= TestConstants.MinPatientAge)
            {
                var doctorsCount = fixture.Appointments
                    .Where(a => a.PatientId == patient.Id)
                    .Select(a => a.DoctorId)
                    .Distinct()
                    .Count();
                Assert.True(doctorsCount < 2);
            }
        });

        Assert.Equal([.. actual.OrderBy(p => p.BirthDate)], actual);
    }

    /// <summary>
    /// ТЕСТ 5: Вывести информацию о приемах за текущий месяц, проходящих в выбранном кабинете
    /// </summary>
    [Fact]
    public void GetAppointmentsCurrentMonthInRoom()
    {
        var testRoom = fixture.Appointments
            .First(a => a.AppointmentDateTime >= TestConstants.StartOfCurrentMonth &&
                       a.AppointmentDateTime < TestConstants.StartOfCurrentMonth.AddMonths(1))
            .RoomNumber;

        var actual = (
            from appointment in fixture.Appointments
            where appointment.RoomNumber == testRoom
            where appointment.AppointmentDateTime >= TestConstants.StartOfCurrentMonth
            where appointment.AppointmentDateTime < TestConstants.StartOfCurrentMonth.AddMonths(1)
            orderby appointment.AppointmentDateTime
            select appointment
        ).ToList();

        Assert.NotEmpty(actual);
        Assert.All(actual, appointment =>
        {
            Assert.Equal(testRoom, appointment.RoomNumber);
            Assert.True(appointment.AppointmentDateTime >= TestConstants.StartOfCurrentMonth);
            Assert.True(appointment.AppointmentDateTime < TestConstants.StartOfCurrentMonth.AddMonths(1));
        });

        var otherRoomAppointments = fixture.Appointments
            .Where(a => a.AppointmentDateTime >= TestConstants.StartOfCurrentMonth)
            .Where(a => a.AppointmentDateTime < TestConstants.StartOfCurrentMonth.AddMonths(1))
            .Where(a => a.RoomNumber != testRoom);

        Assert.All(otherRoomAppointments, a => Assert.NotEqual(testRoom, a.RoomNumber));
        Assert.Equal([.. actual.OrderBy(a => a.AppointmentDateTime)], actual);
    }
}