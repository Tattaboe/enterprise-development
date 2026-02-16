var builder = DistributedApplication.CreateBuilder(args);

var sqlDb = builder.AddSqlServer("polyclinic-sql-server")
                 .AddDatabase("PolyclinicDb");

builder.AddProject<Projects.Polyclinic_Api_Host>("polyclinic-api-host")
    .WithReference(sqlDb, "ConnectionString")
    .WaitFor(sqlDb);

builder.Build().Run();