using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для управления специализациями
/// </summary>
public class SpecializationRepository(PolyclinicDbContext context) : IRepository<Specialization, int>
{
    /// <summary>
    /// Создаёт новую специализацию в базе данных
    /// </summary>
    public async Task<Specialization> Create(Specialization entity)
    {
        await context.Specializations.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Получает специализацию по идентификатору
    /// </summary>
    public async Task<Specialization?> Read(int entityId)
    {
        return await context.Specializations.FirstOrDefaultAsync(e => e.Id == entityId);
    }

    /// <summary>
    /// Получает список всех специализаций
    /// </summary>
    public async Task<IList<Specialization>> ReadAll()
    {
        return await context.Specializations.ToListAsync();
    }

    /// <summary>
    /// Обновляет данные специализации
    /// </summary>
    public async Task<Specialization> Update(Specialization entity)
    {
        context.Specializations.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет специализацию по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Specializations.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
        {
            return false;
        }

        context.Specializations.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}