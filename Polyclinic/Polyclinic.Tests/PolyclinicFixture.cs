using Polyclinic.Domain.Entities;
using Polyclinic.Domain.Enums;

namespace Polyclinic.Tests;

/// <summary>
/// Фикстура с тестовыми данными для поликлиники
/// </summary>
public class PolyclinicFixture
{
    /// <summary>
    /// Список специализаций
    /// </summary>
    public List<Specialization> Specializations { get; }

    /// <summary>
    /// Список врачей
    /// </summary>
    public List<Doctor> Doctors { get; }

    /// <summary>
    /// Список пациентов
    /// </summary>
    public List<Patient> Patients { get; }

    /// <summary>
    /// Список записей на прием
    /// </summary>
    public List<Appointment> Appointments { get; }

    /// <summary>
    /// Конструктор, инициализирующий тестовые данные
    /// </summary>
    public PolyclinicFixture()
    {
        Specializations = GetSpecializations();
        Doctors = GetDoctors();
        Patients = GetPatients();
        Appointments = GetAppointments();

        LinkDoctorsWithSpecializations();
        LinkAppointmentsWithDoctorsAndPatients();
    }

    private List<Specialization> GetSpecializations() =>
    [
        new() { Id = 1, Name = "Терапевт", Code = "THERAPIST", Description = "Врач общей практики" },
        new() { Id = 2, Name = "Хирург", Code = "SURGEON", Description = "Проведение операций" },
        new() { Id = 3, Name = "Кардиолог", Code = "CARDIOLOGIST", Description = "Заболевания сердца" },
        new() { Id = 4, Name = "Невролог", Code = "NEUROLOGIST", Description = "Заболевания нервной системы" },
        new() { Id = 5, Name = "Педиатр", Code = "PEDIATRICIAN", Description = "Детские болезни" },
        new() { Id = 6, Name = "Гинеколог", Code = "GYNECOLOGIST", Description = "Женское здоровье" },
        new() { Id = 7, Name = "Офтальмолог", Code = "OPHTHALMOLOGIST", Description = "Заболевания глаз" },
        new() { Id = 8, Name = "Отоларинголог", Code = "ENT", Description = "Ухо, горло, нос" },
        new() { Id = 9, Name = "Дерматолог", Code = "DERMATOLOGIST", Description = "Кожные заболевания" },
        new() { Id = 10, Name = "Эндокринолог", Code = "ENDOCRINOLOGIST", Description = "Гормональные нарушения" }
    ];

    private List<Doctor> GetDoctors() =>
    [
        new()
        {
            Id = 1,
            PassportNumber = "4501 123456",
            FullName = "Иванов Иван Иванович",
            BirthYear = 1980,
            SpecializationId = 1,
            ExperienceYears = 15
        },
        new()
        {
            Id = 2,
            PassportNumber = "4502 234567",
            FullName = "Петров Петр Петрович",
            BirthYear = 1975,
            SpecializationId = 2,
            ExperienceYears = 20
        },
        new()
        {
            Id = 3,
            PassportNumber = "4503 345678",
            FullName = "Сидорова Анна Сергеевна",
            BirthYear = 1985,
            SpecializationId = 3,
            ExperienceYears = 12
        },
        new()
        {
            Id = 4,
            PassportNumber = "4504 456789",
            FullName = "Козлов Дмитрий Николаевич",
            BirthYear = 1990,
            SpecializationId = 4,
            ExperienceYears = 8
        },
        new()
        {
            Id = 5,
            PassportNumber = "4505 567890",
            FullName = "Морозова Елена Владимировна",
            BirthYear = 1982,
            SpecializationId = 5,
            ExperienceYears = 14
        },
        new()
        {
            Id = 6,
            PassportNumber = "4506 678901",
            FullName = "Волков Андрей Игоревич",
            BirthYear = 1978,
            SpecializationId = 6,
            ExperienceYears = 18
        },
        new()
        {
            Id = 7,
            PassportNumber = "4507 789012",
            FullName = "Соколова Татьяна Александровна",
            BirthYear = 1988,
            SpecializationId = 7,
            ExperienceYears = 10
        },
        new()
        {
            Id = 8,
            PassportNumber = "4508 890123",
            FullName = "Лебедев Михаил Сергеевич",
            BirthYear = 1992,
            SpecializationId = 8,
            ExperienceYears = 6
        },
        new()
        {
            Id = 9,
            PassportNumber = "4509 901234",
            FullName = "Николаева Ольга Викторовна",
            BirthYear = 1983,
            SpecializationId = 9,
            ExperienceYears = 13
        },
        new()
        {
            Id = 10,
            PassportNumber = "4510 012345",
            FullName = "Федоров Алексей Павлович",
            BirthYear = 1970,
            SpecializationId = 10,
            ExperienceYears = 25
        }
    ];

    private List<Patient> GetPatients() =>
    [
        new()
        {
            Id = 1,
            PassportNumber = "6001 123456",
            FullName = "Смирнов Алексей Викторович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1990, 5, 15),
            Address = "ул. Ленина, д. 10, кв. 25",
            BloodGroup = BloodGroup.A,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (999) 123-45-67"
        },
        new()
        {
            Id = 2,
            PassportNumber = "6002 234567",
            FullName = "Кузнецова Елена Дмитриевна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1985, 8, 22),
            Address = "ул. Гагарина, д. 5, кв. 12",
            BloodGroup = BloodGroup.O,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (999) 234-56-78"
        },
        new()
        {
            Id = 3,
            PassportNumber = "6003 345678",
            FullName = "Попов Сергей Иванович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1978, 3, 10),
            Address = "пр. Мира, д. 15, кв. 7",
            BloodGroup = BloodGroup.B,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "+7 (999) 345-67-89"
        },
        new()
        {
            Id = 4,
            PassportNumber = "6004 456789",
            FullName = "Васильева Мария Петровна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1995, 11, 30),
            Address = "ул. Советская, д. 8, кв. 42",
            BloodGroup = BloodGroup.AB,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (999) 456-78-90"
        },
        new()
        {
            Id = 5,
            PassportNumber = "6005 567890",
            FullName = "Соколов Андрей Николаевич",
            Gender = Gender.Male,
            BirthDate = new DateTime(1982, 7, 18),
            Address = "ул. Пушкина, д. 3, кв. 56",
            BloodGroup = BloodGroup.A,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "+7 (999) 567-89-01"
        },
        new()
        {
            Id = 6,
            PassportNumber = "6006 678901",
            FullName = "Михайлова Анна Сергеевна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1975, 9, 25),
            Address = "пр. Ленинградский, д. 22, кв. 15",
            BloodGroup = BloodGroup.O,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (999) 678-90-12"
        },
        new()
        {
            Id = 7,
            PassportNumber = "6007 789012",
            FullName = "Новиков Денис Александрович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1988, 2, 14),
            Address = "ул. Кирова, д. 12, кв. 8",
            BloodGroup = BloodGroup.B,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (999) 789-01-23"
        },
        new()
        {
            Id = 8,
            PassportNumber = "6008 890123",
            FullName = "Морозова Татьяна Владимировна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1992, 6, 5),
            Address = "ул. Садовая, д. 7, кв. 31",
            BloodGroup = BloodGroup.AB,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "+7 (999) 890-12-34"
        },
        new()
        {
            Id = 9,
            PassportNumber = "6009 901234",
            FullName = "Зайцев Игорь Павлович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1970, 12, 3),
            Address = "пр. Невский, д. 45, кв. 19",
            BloodGroup = BloodGroup.A,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (999) 901-23-45"
        },
        new()
        {
            Id = 10,
            PassportNumber = "6010 012345",
            FullName = "Волкова Ольга Игоревна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1965, 4, 20),
            Address = "ул. Комсомольская, д. 6, кв. 23",
            BloodGroup = BloodGroup.O,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (999) 012-34-56"
        },
        new()
        {
            Id = 11,
            PassportNumber = "6011 123456",
            FullName = "Белова Наталья Сергеевна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1998, 1, 8),
            Address = "ул. Мичурина, д. 18, кв. 67",
            BloodGroup = BloodGroup.B,
            RhFactor = RhFactor.Positive,
            PhoneNumber = "+7 (999) 123-56-78"
        },
        new()
        {
            Id = 12,
            PassportNumber = "6012 234567",
            FullName = "Карпов Евгений Владимирович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1983, 9, 12),
            Address = "ул. Лермонтова, д. 9, кв. 14",
            BloodGroup = BloodGroup.AB,
            RhFactor = RhFactor.Negative,
            PhoneNumber = "+7 (999) 234-67-89"
        }
    ];

    private List<Appointment> GetAppointments()
    {
        var appointments = new List<Appointment>();
        var appointmentId = 1;

        // Записи на текущий месяц (февраль 2026)
        appointments.AddRange([
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 2, 5, 10, 0, 0),
                RoomNumber = TestConstants.TherapyRoom,
                IsRepeat = false,
                PatientId = 1,
                DoctorId = 1
            },
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 2, 5, 11, 0, 0),
                RoomNumber = TestConstants.TherapyRoom,
                IsRepeat = true,
                PatientId = 2,
                DoctorId = 1
            },
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 2, 10, 14, 30, 0),
                RoomNumber = TestConstants.SurgeryRoom,
                IsRepeat = false,
                PatientId = 3,
                DoctorId = 2
            },
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 2, 15, 9, 15, 0),
                RoomNumber = TestConstants.CardiologyRoom,
                IsRepeat = false,
                PatientId = 4,
                DoctorId = 3
            },
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 2, 12, 13, 30, 0),
                RoomNumber = TestConstants.TherapyRoom,
                IsRepeat = false,
                PatientId = 8,
                DoctorId = 1
            },
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 2, 16, 9, 30, 0),
                RoomNumber = TestConstants.TherapyRoom,
                IsRepeat = false,
                PatientId = 11,
                DoctorId = 1
            }
        ]);

        // Записи на прошлый месяц (январь 2026)
        appointments.AddRange([
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 1, 15, 10, 0, 0),
                RoomNumber = TestConstants.TherapyRoom,
                IsRepeat = true,
                PatientId = 6,
                DoctorId = 2
            },
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 1, 20, 11, 0, 0),
                RoomNumber = TestConstants.SurgeryRoom,
                IsRepeat = true,
                PatientId = 7,
                DoctorId = 2
            },
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 1, 5, 9, 0, 0),
                RoomNumber = TestConstants.CardiologyRoom,
                IsRepeat = false,
                PatientId = 8,
                DoctorId = 3
            }
        ]);

        // Пациент Соколов (Id=5) записан к нескольким врачам
        appointments.AddRange([
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 2, 8, 9, 0, 0),
                RoomNumber = "102",
                IsRepeat = false,
                PatientId = 5,
                DoctorId = 2
            },
            new()
            {
                Id = appointmentId++,
                AppointmentDateTime = new DateTime(2026, 2, 22, 11, 30, 0),
                RoomNumber = "606",
                IsRepeat = false,
                PatientId = 5,
                DoctorId = 6
            }
        ]);

        return appointments;
    }

    private void LinkDoctorsWithSpecializations()
    {
        foreach (var doctor in Doctors)
        {
            doctor.Specialization = Specializations.First(s => s.Id == doctor.SpecializationId);
            doctor.Specialization.Doctors.Add(doctor);
        }
    }

    private void LinkAppointmentsWithDoctorsAndPatients()
    {
        foreach (var appointment in Appointments)
        {
            appointment.Doctor = Doctors.First(d => d.Id == appointment.DoctorId);
            appointment.Patient = Patients.First(p => p.Id == appointment.PatientId);

            appointment.Doctor.Appointments.Add(appointment);
            appointment.Patient.Appointments.Add(appointment);
        }
    }
}