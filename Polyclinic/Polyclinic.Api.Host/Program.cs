using Microsoft.EntityFrameworkCore;
using Polyclinic.Application;
using Polyclinic.Application.Contracts;
using Polyclinic.Application.Contracts.Appointments;
using Polyclinic.Application.Contracts.Doctors;
using Polyclinic.Application.Contracts.Patients;
using Polyclinic.Application.Contracts.Specializations;
using Polyclinic.Application.Services;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;
using Polyclinic.Infrastructure.EfCore;
using Polyclinic.Infrastructure.EfCore.Repositories;
using Polyclinic.Infrastructure.RabbitMq;
using Polyclinic.ServiceDefaults;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new PolyclinicProfile());
});

builder.Services.AddSingleton<PolyclinicFixture>();

builder.AddServiceDefaults();

builder.Services.AddTransient<IRepository<Specialization, int>, SpecializationRepository>();
builder.Services.AddTransient<IRepository<Patient, int>, PatientRepository>();
builder.Services.AddTransient<IRepository<Doctor, int>, DoctorRepository>();
builder.Services.AddTransient<IRepository<Appointment, int>, AppointmentRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>, SpecializationService>();
builder.Services.AddScoped<IApplicationService<PatientDto, PatientCreateUpdateDto, int>, PatientService>();
builder.Services.AddScoped<IApplicationService<DoctorDto, DoctorCreateUpdateDto, int>, DoctorService>();
builder.Services.AddScoped<IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>, AppointmentService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("Polyclinic"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }

    c.UseInlineDefinitionsForEnums();
});

builder.AddSqlServerDbContext<PolyclinicDbContext>("DatabaseConnectionString");

builder.AddRabbitMQClient("rabbitMqConnection");

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));

builder.Services.AddHostedService<AppointmentConsumer>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<PolyclinicDbContext>();
    await db.Database.MigrateAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
