namespace Polyclinic.Application.Contracts.Analytics;

/// <summary>
/// DTO со статистикой приемов за месяц
/// </summary>
/// <param name="Year">Год статистики</param>
/// <param name="Month">Месяц статистики</param>
/// <param name="RepeatAppointmentCount">Количество повторных приемов</param>
/// <param name="TotalAppointmentCount">Общее количество приемов</param>
public record MonthlyAppointmentStatsDto(
    int Year,
    int Month,
    int RepeatAppointmentCount,
    int TotalAppointmentCount
);