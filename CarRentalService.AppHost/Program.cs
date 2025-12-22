using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var dbName = "carrentaldb";
var postgres = builder.AddPostgres("postgres")
    .WithEnvironment("POSTGRES_DB", dbName);

var carRentalDb = postgres.AddDatabase(dbName);

builder.AddProject<Projects.CarRentalService_Api_Host>("api")
    .WithReference(carRentalDb, "Database")
    .WaitFor(carRentalDb);

var kafka = builder.AddKafka("car-rental-kafka")
    .WithKafkaUI();

var kafkaSettings = builder.Configuration.GetSection("Kafka");
var generatorSettings = builder.Configuration.GetSection("Generator");

var generator = builder.AddProject<Projects.CarRentalService_Generator_Kafka>("generator")
    .WithReference(kafka)
    .WaitFor(kafka)
    .WaitFor(carRentalDb)
    .WithEnvironment("Kafka:Topic", kafkaSettings["Topic"] ?? "car-rental.rentals")
    .WithEnvironment("Generator:BatchSize", generatorSettings.GetValue("BatchSize", 10).ToString())
    .WithEnvironment("Generator:PayloadLimit", generatorSettings.GetValue("PayloadLimit", 100).ToString())
    .WithEnvironment("Generator:WaitTime", generatorSettings.GetValue("WaitTime", 5).ToString());

builder.AddProject<Projects.CarRentalService_Infrastructure_Kafka>("consumer")
    .WithReference(kafka)
    .WithReference(carRentalDb, "Database")
    .WaitFor(kafka)
    .WaitFor(generator)
    .WithEnvironment("Kafka:GroupId", kafkaSettings["GroupId"] ?? "car-rental-consumer-group")
    .WithEnvironment("Kafka:Topic", kafkaSettings["Topic"] ?? "car-rental.rentals");

builder.Build().Run();