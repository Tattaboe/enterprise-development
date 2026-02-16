using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Patients;

namespace Polyclinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления данными пациентов
/// </summary>
public class PatientsController(
    IApplicationService<PatientDto, PatientCreateUpdateDto, int> service)
    : CrudControllerBase<PatientDto, PatientCreateUpdateDto, int>(service);