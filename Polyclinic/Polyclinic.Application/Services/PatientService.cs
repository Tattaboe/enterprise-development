using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для управления пациентами
/// </summary>
public class PatientService(
    IRepository<Patient, int> repository,
    IMapper mapper)
    : IApplicationService<PatientDto, PatientCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<PatientDto> Create(PatientCreateUpdateDto dto)
    {
        var entity = mapper.Map<Patient>(dto);
        var result = await repository.Create(entity);
        return mapper.Map<PatientDto>(result);
    }

    /// <inheritdoc/>
    public async Task<PatientDto?> Get(int dtoId)
    {
        var entity = await repository.Read(dtoId)
                     ?? throw new KeyNotFoundException($"Пациент с id {dtoId} не найден.");

        return mapper.Map<PatientDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<PatientDto>> GetAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<IList<PatientDto>>(entities);
    }

    /// <inheritdoc/>
    public async Task<PatientDto> Update(PatientCreateUpdateDto dto, int dtoId)
    {
        var entity = await repository.Read(dtoId)
                     ?? throw new KeyNotFoundException($"Пациент с id {dtoId} не найден.");

        mapper.Map(dto, entity);

        var result = await repository.Update(entity);
        return mapper.Map<PatientDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) => await repository.Delete(dtoId);
}