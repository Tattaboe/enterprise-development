using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Doctors;

namespace Polyclinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления данными врачей
/// </summary>
public class DoctorController(
    IApplicationService<DoctorDto, DoctorCreateUpdateDto, int> service)
    : CrudControllerBase<DoctorDto, DoctorCreateUpdateDto, int>(service);