using Bogus;
using Microsoft.Extensions.Options;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Generator.RabbitMq.Host.Options;

namespace Polyclinic.Generator.RabbitMq.Host.Services;

/// <summary>
/// Сервис для генерации фейковых данных о записях на прием
/// </summary>
public class AppointmentGenerator
{
    private readonly Faker<AppointmentCreateUpdateDto> _faker;

    /// <summary>
    /// Инициализирует новый экземпляр генератора с заданными настройками
    /// </summary>
    /// <param name="options">Настройки генерации (диапазоны ID, дат и т.д.)</param>
    public AppointmentGenerator(IOptions<GeneratorOptions> options)
    {
        var configuration = options.Value;

        _faker = new Faker<AppointmentCreateUpdateDto>()
            .CustomInstantiator(f => new AppointmentCreateUpdateDto(
                AppointmentDateTime: f.Date.Between(configuration.StartDate, configuration.EndDate),
                RoomNumber: f.Random.Int(configuration.MinRoomNumber, configuration.MaxRoomNumber).ToString(),
                IsRepeat: f.Random.Bool(configuration.RepeatProbability),
                PatientId: f.Random.Int(configuration.MinPatientId, configuration.MaxPatientId),
                DoctorId: f.Random.Int(configuration.MinDoctorId, configuration.MaxDoctorId)
            ));
    }

    /// <summary>
    /// Генерирует указанное количество записей на прием
    /// </summary>
    /// <param name="count">Количество записей для генерации</param>
    /// <returns>Коллекция DTO для создания записей</returns>
    public IEnumerable<AppointmentCreateUpdateDto> Generate(int count)
    {
        return _faker.Generate(count);
    }
}