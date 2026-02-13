using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Polyclinic.Domain.Enums;

namespace Polyclinic.Domain.Entities;

/// <summary>
/// Пациент поликлиники
/// </summary>
public class Patient
{
    /// <summary>
    /// Уникальный идентификатор пациента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта (уникальный)
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО пациента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол пациента
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Адрес проживания
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Группа крови
    /// </summary>
    public BloodGroup BloodGroup { get; set; }

    /// <summary>
    /// Резус-фактор
    /// </summary>
    public RhFactor RhFactor { get; set; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Список записей на прием этого пациента
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];

    /// <summary>
    /// Вычисление возраста пациента на указанную дату
    /// </summary>
    public int GetAge(DateTime onDate)
    {
        var age = onDate.Year - BirthDate.Year;
        if (BirthDate.Date > onDate.AddYears(-age)) age--;
        return age;
    }
}
