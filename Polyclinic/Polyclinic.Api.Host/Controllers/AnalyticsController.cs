using Microsoft.AspNetCore.Mvc;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Analytics;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;

namespace Polyclinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для получения аналитических отчетов и выборок
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Получить врачей со стажем работы более указанного (по умолчанию 10 лет)
    /// </summary>
    /// <param name="minExperience">Минимальный стаж в годах</param>
    [HttpGet("doctors/experienced")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<DoctorDto>>> GetExperiencedDoctors([FromQuery] int minExperience = 10)
    {
        try
        {
            var result = await analyticsService.GetDoctorsWithExperienceAsync(minExperience);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    /// <summary>
    /// Получить список пациентов, записанных к конкретному врачу, отсортированный по ФИО
    /// </summary>
    /// <param name="doctorId">Идентификатор врача</param>
    [HttpGet("doctors/{doctorId}/patients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<PatientDto>>> GetPatientsByDoctor(int doctorId)
    {
        try
        {
            var result = await analyticsService.GetPatientsByDoctorAsync(doctorId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    /// <summary>
    /// Получить статистику повторных приемов за указанный месяц
    /// </summary>
    /// <param name="date">Дата для определения месяца и года</param>
    [HttpGet("appointments/stats/monthly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MonthlyAppointmentStatsDto>> GetMonthlyStats([FromQuery] DateTime date)
    {
        try
        {
            var result = await analyticsService.GetMonthlyRepeatStatsAsync(date);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    /// <summary>
    /// Получить пациентов старше указанного возраста (по умолчанию 30), посетивших более одного врача
    /// </summary>
    /// <param name="minAge">Минимальный возраст</param>
    [HttpGet("patients/active-visitors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<PatientDto>>> GetPatientsWithMultipleDoctors([FromQuery] int minAge = 30)
    {
        try
        {
            var result = await analyticsService.GetPatientsWithMultipleDoctorsAsync(minAge);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }

    /// <summary>
    /// Получить список приемов в конкретном кабинете за месяц
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="date">Дата для определения месяца выборки</param>
    [HttpGet("rooms/{roomNumber}/appointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<AppointmentDto>>> GetAppointmentsByRoom(string roomNumber, [FromQuery] DateTime date)
    {
        try
        {
            var result = await analyticsService.GetAppointmentsByRoomAsync(roomNumber, date);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
        }
    }
}