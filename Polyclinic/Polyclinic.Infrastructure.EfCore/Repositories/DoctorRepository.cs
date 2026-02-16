using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для управления врачами
/// </summary>
public class DoctorRepository(PolyclinicDbContext context) : IRepository<Doctor, int>
{
    /// <summary>
    /// Создаёт нового врача в базе данных
    /// </summary>
    public async Task<Doctor> Create(Doctor entity)
    {
        await context.Doctors.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Получает врача по идентификатору вместе со специализацией
    /// </summary>
    public async Task<Doctor?> Read(int entityId)
    {
        return await context.Doctors
            .Include(d => d.Specialization)
            .FirstOrDefaultAsync(d => d.Id == entityId);
    }

    /// <summary>
    /// Получает список всех врачей вместе с их специализациями
    /// </summary>
    public async Task<IList<Doctor>> ReadAll()
    {
        return await context.Doctors
            .Include(d => d.Specialization)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет данные врача
    /// </summary>
    public async Task<Doctor> Update(Doctor entity)
    {
        context.Doctors.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет врача по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Doctors.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
        {
            return false;
        }

        context.Doctors.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}