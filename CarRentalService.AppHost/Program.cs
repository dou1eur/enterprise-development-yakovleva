var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DatabasePassword");
var dbName = "carrentaldb";

var postgres = builder.AddPostgres("postgres", password: password)
    .WithEnvironment("POSTGRES_DB", dbName);

var carRentalDb = postgres.AddDatabase(dbName);

builder.AddProject<Projects.CarRentalService_Api_Host>("api")
    .WithReference(carRentalDb, "Database")
    .WaitFor(carRentalDb);

builder.Build().Run();