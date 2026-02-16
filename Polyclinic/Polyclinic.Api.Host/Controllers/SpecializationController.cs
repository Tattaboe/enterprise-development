using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Specializations;

namespace Polyclinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления справочником специализаций
/// </summary>
public class SpecializationController(
    IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int> service)
    : CrudControllerBase<SpecializationDto, SpecializationCreateUpdateDto, int>(service);