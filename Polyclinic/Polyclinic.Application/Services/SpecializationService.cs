using AutoMapper;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Specializations;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Application.Services;

/// <summary>
/// Сервис для управления специализациями
/// </summary>
public class SpecializationService(
    IRepository<Specialization, int> repository,
    IMapper mapper)
    : IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<SpecializationDto> Create(SpecializationCreateUpdateDto dto)
    {
        var entity = mapper.Map<Specialization>(dto);
        var result = await repository.Create(entity);
        return mapper.Map<SpecializationDto>(result);
    }

    /// <inheritdoc/>
    public async Task<SpecializationDto?> Get(int dtoId)
    {
        var entity = await repository.Read(dtoId)
                     ?? throw new KeyNotFoundException($"Специализация с id {dtoId} не найдена.");

        return mapper.Map<SpecializationDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<SpecializationDto>> GetAll()
    {
        var entities = await repository.ReadAll();
        return mapper.Map<IList<SpecializationDto>>(entities);
    }

    /// <inheritdoc/>
    public async Task<SpecializationDto> Update(SpecializationCreateUpdateDto dto, int dtoId)
    {
        var entity = await repository.Read(dtoId)
                     ?? throw new KeyNotFoundException($"Специализация с id {dtoId} не найдена.");

        mapper.Map(dto, entity);

        var result = await repository.Update(entity);
        return mapper.Map<SpecializationDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) => await repository.Delete(dtoId);
}