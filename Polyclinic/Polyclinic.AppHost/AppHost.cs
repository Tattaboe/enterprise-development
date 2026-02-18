var builder = DistributedApplication.CreateBuilder(args);

var sqlDb = builder.AddSqlServer("polyclinic-sql-server")
                 .AddDatabase("PolyclinicDb");

var rabbitmq = builder.AddRabbitMQ("rabbitMqConnection")
    .WithManagementPlugin();

builder.AddProject<Projects.Polyclinic_Api_Host>("polyclinic-api-host")
    .WithReference(sqlDb, "DatabaseConnectionString")
    .WithReference(rabbitmq)
    .WaitFor(sqlDb)
    .WaitFor(rabbitmq);

builder.AddProject<Projects.Polyclinic_Generator_RabbitMq_Host>("polyclinic-generator")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq);

builder.Build().Run();