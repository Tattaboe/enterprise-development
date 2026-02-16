using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для управления записями на прием
/// </summary>
public class AppointmentRepository(PolyclinicDbContext context) : IRepository<Appointment, int>
{
    /// <summary>
    /// Создаёт новую запись на прием в базе данных
    /// </summary>
    public async Task<Appointment> Create(Appointment entity)
    {
        await context.Appointments.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Получает запись по идентификатору с данными о враче и пациенте
    /// </summary>
    public async Task<Appointment?> Read(int entityId)
    {
        return await context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d!.Specialization)
            .FirstOrDefaultAsync(a => a.Id == entityId);
    }

    /// <summary>
    /// Получает список всех записей с данными о врачах и пациентах
    /// </summary>
    public async Task<IList<Appointment>> ReadAll()
    {
        return await context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
                .ThenInclude(d => d!.Specialization)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет данные записи на прием
    /// </summary>
    public async Task<Appointment> Update(Appointment entity)
    {
        context.Appointments.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет запись на прием по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Appointments.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
        {
            return false;
        }

        context.Appointments.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}