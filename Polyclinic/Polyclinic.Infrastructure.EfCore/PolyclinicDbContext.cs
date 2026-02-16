using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain;
using Polyclinic.Domain.Entities;

namespace Polyclinic.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных поликлиники
/// </summary>
public class PolyclinicDbContext(
    DbContextOptions<PolyclinicDbContext> options,
    PolyclinicFixture fixture) : DbContext(options)
{
    /// <summary>
    /// Таблица специализаций врачей (справочник)
    /// </summary>
    public DbSet<Specialization> Specializations { get; set; }

    /// <summary>
    /// Таблица врачей поликлиники
    /// </summary>
    public DbSet<Doctor> Doctors { get; set; }

    /// <summary>
    /// Таблица пациентов
    /// </summary>
    public DbSet<Patient> Patients { get; set; }

    /// <summary>
    /// Таблица записей на прием (журнал посещений)
    /// </summary>
    public DbSet<Appointment> Appointments { get; set; }

    /// <summary>
    /// Настройка модели базы данных при её создании
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureSpecialization(modelBuilder);
        ConfigureDoctor(modelBuilder);
        ConfigurePatient(modelBuilder);
        ConfigureAppointment(modelBuilder);
    }

    /// <summary>
    /// Конфигурация сущности <see cref="Specialization"/>
    /// </summary>
    private void ConfigureSpecialization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            if (fixture.Specializations.Count > 0)
            {
                entity.HasData(fixture.Specializations);
            }
        });
    }

    /// <summary>
    /// Конфигурация сущности <see cref="Doctor"/>
    /// </summary>
    private void ConfigureDoctor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.PassportNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

            if (fixture.Doctors.Count > 0)
            {
                entity.HasData(fixture.Doctors);
            }
        });
    }

    /// <summary>
    /// Конфигурация сущности <see cref="Patient"/>
    /// </summary>
    private void ConfigurePatient(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.PassportNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Address)
                .HasMaxLength(250);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            if (fixture.Patients.Count > 0)
            {
                entity.HasData(fixture.Patients);
            }
        });
    }

    /// <summary>
    /// Конфигурация сущности <see cref="Appointment"/>
    /// </summary>
    private void ConfigureAppointment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.RoomNumber)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(e => e.AppointmentDateTime)
                .HasColumnType("datetime2");

            entity.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            if (fixture.Appointments.Count > 0)
            {
                entity.HasData(fixture.Appointments);
            }
        });
    }
}