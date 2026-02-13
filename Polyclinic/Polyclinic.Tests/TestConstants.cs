namespace Polyclinic.Tests;

/// <summary>
/// Константы для тестов
/// </summary>
public static class TestConstants
{
    /// <summary>
    /// Фиксированная дата для тестов (15 февраля 2026)
    /// </summary>
    public static readonly DateTime Today = new(2026, 2, 15);

    /// <summary>
    /// Начало текущего месяца
    /// </summary>
    public static readonly DateTime StartOfCurrentMonth = new(Today.Year, Today.Month, 1);

    /// <summary>
    /// Начало прошлого месяца
    /// </summary>
    public static readonly DateTime StartOfLastMonth = StartOfCurrentMonth.AddMonths(-1);

    /// <summary>
    /// Минимальный стаж для опытных врачей
    /// </summary>
    public const int MinExperienceYears = 10;

    /// <summary>
    /// Минимальный возраст для "пациенты старше 30 лет"
    /// </summary>
    public const int MinPatientAge = 30;

    /// <summary>
    /// Номер кабинета терапевта
    /// </summary>
    public const string TherapyRoom = "101";

    /// <summary>
    /// Номер кабинета хирурга
    /// </summary>
    public const string SurgeryRoom = "202";

    /// <summary>
    /// Номер кабинета кардиолога
    /// </summary>
    public const string CardiologyRoom = "303";
}