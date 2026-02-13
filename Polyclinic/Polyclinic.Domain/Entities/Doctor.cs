using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Polyclinic.Domain.Enums;

namespace Polyclinic.Domain.Entities;

/// <summary>
/// Врач поликлиники
/// </summary>
public class Doctor
{
    /// <summary>
    /// Уникальный идентификатор врача
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта (уникальный)
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО врача
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Год рождения
    /// </summary>
    public int BirthYear { get; set; }

    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Стаж работы (в годах)
    /// </summary>
    public int ExperienceYears { get; set; }

    /// <summary>
    /// Навигационное свойство: специализация
    /// </summary>
    public Specialization? Specialization { get; set; }

    /// <summary>
    /// Список приемов у этого врача
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];

    /// <summary>
    /// Вычисление возраста врача на указанную дату
    /// </summary>
    public int GetAge(DateTime onDate) => onDate.Year - BirthYear;
}