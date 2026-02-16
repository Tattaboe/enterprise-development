using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для управления пациентами
/// </summary>
public class PatientRepository(PolyclinicDbContext context) : IRepository<Patient, int>
{
    /// <summary>
    /// Создаёт нового пациента в базе данных
    /// </summary>
    public async Task<Patient> Create(Patient entity)
    {
        await context.Patients.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Получает пациента по идентификатору
    /// </summary>
    public async Task<Patient?> Read(int entityId)
    {
        return await context.Patients.FirstOrDefaultAsync(e => e.Id == entityId);
    }

    /// <summary>
    /// Получает список всех пациентов
    /// </summary>
    public async Task<IList<Patient>> ReadAll()
    {
        return await context.Patients.ToListAsync();
    }

    /// <summary>
    /// Обновляет данные пациента
    /// </summary>
    public async Task<Patient> Update(Patient entity)
    {
        context.Patients.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет пациента по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Patients.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
        {
            return false;
        }

        context.Patients.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}