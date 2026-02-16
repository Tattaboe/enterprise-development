using AutoMapper;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Contracts.Specializations;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Application;

/// <summary>
/// Профиль маппинга для преобразования данных между доменными сущностями и DTO
/// </summary>
public class PolyclinicProfile : Profile
{
    public PolyclinicProfile()
    {
        CreateMap<Specialization, SpecializationDto>();
        CreateMap<SpecializationCreateUpdateDto, Specialization>();

        CreateMap<Doctor, DoctorDto>();
        CreateMap<DoctorCreateUpdateDto, Doctor>();

        CreateMap<Patient, PatientDto>();
        CreateMap<PatientCreateUpdateDto, Patient>();

        CreateMap<Appointment, AppointmentDto>();
        CreateMap<AppointmentCreateUpdateDto, Appointment>();
    }
}