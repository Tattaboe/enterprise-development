using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyclinic.Domain.Entities;

/// <summary>
/// Запись пациента на прием к врачу
/// </summary>
public class Appointment
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время приема
    /// </summary>
    public DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    public required string RoomNumber { get; set; }

    /// <summary>
    /// Флаг повторного приема
    /// </summary>
    public bool IsRepeat { get; set; }

    /// <summary>
    /// Идентификатор пациента
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Идентификатор врача
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Навигационное свойство: пациент
    /// </summary>
    public Patient? Patient { get; set; }

    /// <summary>
    /// Навигационное свойство: врач
    /// </summary>
    public Doctor? Doctor { get; set; }
}