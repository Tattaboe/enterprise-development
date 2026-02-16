using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для управления врачами
/// </summary>
public class DoctorService(
    IRepository<Doctor, int> doctorRepository,
    IRepository<Specialization, int> specializationRepository,
    IMapper mapper)
    : IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<DoctorDto> Create(DoctorCreateUpdateDto dto)
    {
        _ = await specializationRepository.Read(dto.SpecializationId)
            ?? throw new KeyNotFoundException($"Специализация с id {dto.SpecializationId} не найдена.");

        var entity = mapper.Map<Doctor>(dto);
        var result = await doctorRepository.Create(entity);
        return mapper.Map<DoctorDto>(result);
    }

    /// <inheritdoc/>
    public async Task<DoctorDto?> Get(int dtoId)
    {
        var entity = await doctorRepository.Read(dtoId)
                     ?? throw new KeyNotFoundException($"Врач с id {dtoId} не найден.");

        return mapper.Map<DoctorDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<DoctorDto>> GetAll()
    {
        var entities = await doctorRepository.ReadAll();
        return mapper.Map<IList<DoctorDto>>(entities);
    }

    /// <inheritdoc/>
    public async Task<DoctorDto> Update(DoctorCreateUpdateDto dto, int dtoId)
    {
        _ = await specializationRepository.Read(dto.SpecializationId)
            ?? throw new KeyNotFoundException($"Специализация с id {dto.SpecializationId} не найдена.");

        var entity = await doctorRepository.Read(dtoId)
                     ?? throw new KeyNotFoundException($"Врач с id {dtoId} не найден.");

        mapper.Map(dto, entity);

        var result = await doctorRepository.Update(entity);
        return mapper.Map<DoctorDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) => await doctorRepository.Delete(dtoId);
}