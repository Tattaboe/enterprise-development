using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;

namespace Polyclinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления записями на прием
/// </summary>
public class AppointmentsController(
    IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int> service)
    : CrudControllerBase<AppointmentDto, AppointmentCreateUpdateDto, int>(service);