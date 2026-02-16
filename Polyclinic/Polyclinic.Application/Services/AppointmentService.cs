using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для управления записями на прием
/// </summary>
public class AppointmentService(
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Doctor, int> doctorRepository,
    IRepository<Patient, int> patientRepository,
    IMapper mapper)
    : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<AppointmentDto> Create(AppointmentCreateUpdateDto dto)
    {
        _ = await patientRepository.Read(dto.PatientId)
            ?? throw new KeyNotFoundException($"Пациент с id {dto.PatientId} не найден.");

        _ = await doctorRepository.Read(dto.DoctorId)
            ?? throw new KeyNotFoundException($"Врач с id {dto.DoctorId} не найден.");

        var entity = mapper.Map<Appointment>(dto);
        var result = await appointmentRepository.Create(entity);
        return mapper.Map<AppointmentDto>(result);
    }

    /// <inheritdoc/>
    public async Task<AppointmentDto?> Get(int dtoId)
    {
        var entity = await appointmentRepository.Read(dtoId)
                     ?? throw new KeyNotFoundException($"Запись на прием с id {dtoId} не найдена.");

        return mapper.Map<AppointmentDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<AppointmentDto>> GetAll()
    {
        var entities = await appointmentRepository.ReadAll();
        return mapper.Map<IList<AppointmentDto>>(entities);
    }

    /// <inheritdoc/>
    public async Task<AppointmentDto> Update(AppointmentCreateUpdateDto dto, int dtoId)
    {
        _ = await patientRepository.Read(dto.PatientId)
            ?? throw new KeyNotFoundException($"Пациент с id {dto.PatientId} не найден.");

        _ = await doctorRepository.Read(dto.DoctorId)
            ?? throw new KeyNotFoundException($"Врач с id {dto.DoctorId} не найден.");

        var entity = await appointmentRepository.Read(dtoId)
                     ?? throw new KeyNotFoundException($"Запись на прием с id {dtoId} не найдена.");

        mapper.Map(dto, entity);

        var result = await appointmentRepository.Update(entity);
        return mapper.Map<AppointmentDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) => await appointmentRepository.Delete(dtoId);
}