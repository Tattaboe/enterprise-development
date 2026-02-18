# Разработка корпоративных приложений. Лабораторные работы №1-4

## Лабораторная работа №1

### Цель
Реализация объектной модели данных и unit-тестов.

### Описание
Необходимо подготовить структуру классов, описывающих предметную область. В каждом из заданий присутствует часть, связанная с обработкой данных, представленная в разделе «Unit-тесты». Данную часть необходимо реализовать в виде unit-тестов: подготовить тестовые данные, выполнить запрос с использованием LINQ, проверить результаты. Хранение данных на этом этапе допускается осуществлять в памяти в виде коллекций.

### Предметная область - Поликлиника
В базе данных поликлиники содержится информация о записях пациентов на прием к врачам.

Пациент характеризуется: номером паспорта, ФИО, полом, датой рождения, адресом, группой крови, резус фактором и контактным телефоном.
Пол пациента является перечислением.
Группа крови пациента является перечислением.
Резус фактор пациента является перечислением.

Информация о враче включает номер паспорта, ФИО, год рождения, специализацию, стаж работы. Специализация врача является справочником.

При записи на прием пациента в базе данных фиксируется дата и время приема, номер кабинета, а также индикатор того, является ли прием повторным.
Используется в качестве контракта.

### Юнит-тесты

- Вывести информацию о всех врачах, стаж работы которых не менее 10 лет.
- Вывести информацию о всех пациентах, записанных на прием к указанному врачу, упорядочить по ФИО.
- Вывести информацию о количестве повторных приемов пациентов за последний месяц.
- Вывести информацию о пациентах старше 30 лет, которые записаны на прием к нескольким врачам, упорядочить по дате рождения.
- Вывести информацию о приемах за текущий месяц, проходящих в выбранном кабинете.

## Лабораторная работа №2

### Цель
Реализация серверного приложения с CRUD-операциями и аналитикой.

### Реализация
Создано Web API на ASP.NET Core со следующими возможностями:

**CRUD-контроллеры:**
- `PatientController` - управление пациентами
- `DoctorController` - управление врачами
- `SpecializationController` - управление специализациями
- `AppointmentController` - управление записями на прием

**Аналитический контроллер (`AnalyticsController`):**
- `GET /api/analytics/doctors/experienced` - врачи со стажем ≥ 10 лет
- `GET /api/analytics/doctors/{doctorId}/patients` - пациенты врача (сортировка по ФИО)
- `GET /api/analytics/appointments/stats/monthly` - статистика повторных приемов за месяц
- `GET /api/analytics/patients/multiple-doctors` - пациенты старше 30 лет у нескольких врачей
- `GET /api/analytics/appointments/by-room` - приемы в кабинете за текущий месяц

**Слой приложения:**
- DTO для передачи данных (CreateUpdateDto, Dto)
- Сервисы с бизнес-логикой
- AutoMapper для маппинга сущностей

## Лабораторная работа №3

### Цель
Подключение базы данных и оркестрация запуска.

### Реализация
**Entity Framework Core:**
- `PolyclinicDbContext` - контекст базы данных
- Репозитории для каждой сущности
- Миграция `InitialCreate` с созданием таблиц и начальными данными

**Aspire оркестратор (`Polyclinic.AppHost`):**
- SQL Server контейнер (`polyclinic-sql-server`)
- База данных `PolyclinicDb`
- API Host с зависимостью от БД

## Лабораторная работа №4

### Цель
Реализация сервиса генерации контрактов с использованием брокера сообщений.

### Реализация
**Генератор записей (`Polyclinic.Generator.RabbitMq.Host`):**
- Отдельное приложение без зависимостей от серверных проектов
- `AppointmentGenerator` — генерация тестовых данных записей на прием
- `AppointmentProducer` — отправка сообщений в очередь RabbitMQ
- `GeneratorController` — API для управления генерацией

**Инфраструктура RabbitMQ (`Polyclinic.Infrastructure.RabbitMq`):**
- `AppointmentConsumer` — фоновый сервис чтения сообщений из очереди
- `RabbitMqOptions` — конфигурация подключения к брокеру
- Автоматическое сохранение полученных контрактов в БД

**Aspire оркестратор (обновлен):**
- RabbitMQ контейнер (`rabbitMqConnection`) с плагином управления
- Генератор (`polyclinic-generator`) с зависимостью от RabbitMQ
- API Host с зависимостями от SQL Server и RabbitMQ

## Структура проекта

```
Polyclinic (Solution)
│
├── Polyclinic.Domain (Class Library) - Доменные сущности
│   ├── Entities/
│   │   ├── Appointment.cs
│   │   ├── Doctor.cs
│   │   ├── Patient.cs
│   │   └── Specialization.cs
│   ├── Enums/
│   │   ├── BloodGroup.cs
│   │   ├── Gender.cs
│   │   └── RhFactor.cs
│   └── IRepository.cs
│
├── Polyclinic.Application.Contracts (Class Library) - Контракты и DTO
│   ├── IApplicationService.cs
│   ├── IAnalyticsService.cs
│   ├── Appointments/
│   │   ├── AppointmentDto.cs
│   │   └── AppointmentCreateUpdateDto.cs
│   ├── Doctors/
│   │   ├── DoctorDto.cs
│   │   └── DoctorCreateUpdateDto.cs
│   ├── Patients/
│   │   ├── PatientDto.cs
│   │   └── PatientCreateUpdateDto.cs
│   ├── Specializations/
│   │   ├── SpecializationDto.cs
│   │   └── SpecializationCreateUpdateDto.cs
│   └── Analytics/
│       └── MonthlyAppointmentStatsDto.cs
│
├── Polyclinic.Application (Class Library) - Реализация сервисов
│   ├── PolyclinicProfile.cs (AutoMapper)
│   └── Services/
│       ├── AnalyticsService.cs
│       ├── AppointmentService.cs
│       ├── DoctorService.cs
│       ├── PatientService.cs
│       └── SpecializationService.cs
│
├── Polyclinic.Infrastructure.EfCore (Class Library) - Инфраструктура БД
│   ├── PolyclinicDbContext.cs
│   ├── Migrations/
│   │   └── InitialCreate.cs
│   └── Repositories/
│       ├── AppointmentRepository.cs
│       ├── DoctorRepository.cs
│       ├── PatientRepository.cs
│       └── SpecializationRepository.cs
│
├── Polyclinic.Api.Host (ASP.NET Core Web API) - HTTP API
│   ├── Program.cs
│   └── Controllers/
│       ├── CrudControllerBase.cs
│       ├── AnalyticsController.cs
│       ├── AppointmentController.cs
│       ├── DoctorController.cs
│       ├── PatientController.cs
│       └── SpecializationController.cs
│
├── Polyclinic.Infrastructure.RabbitMq (Class Library) - Интеграция с RabbitMQ
│   ├── AppointmentConsumer.cs
│   └── RabbitMqOptions.cs
│
├── Polyclinic.Generator.RabbitMq.Host (ASP.NET Core) - Генератор контрактов
│   ├── Program.cs
│   ├── Controllers/
│   │   └── GeneratorController.cs
│   ├── Services/
│   │   ├── AppointmentGenerator.cs
│   │   └── AppointmentProducer.cs
│   └── Options/
│
├── Polyclinic.AppHost (Aspire Host) - Оркестратор
│   └── AppHost.cs
│
├── Polyclinic.ServiceDefaults (Class Library) - Общие настройки Aspire
│   └── Extensions.cs
│
└── Polyclinic.Tests (xUnit) - Модульные тесты
    ├── PolyclinicFixture.cs
    ├── PolyclinicTests.cs
    └── TestConstants.cs
```