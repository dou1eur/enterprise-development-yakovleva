using CarRentalService.Application;
using CarRentalService.Infrastructure;
using CarRentalService.Infrastructure.Kafka;
using CarRentalService.Infrastructure.Kafka.Deserializers;
using CarRentalService.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddSingleton<KeyDeserializer>();
builder.Services.AddSingleton<ValueDeserializer>();

builder.Services.AddHostedService<KafkaConsumer>();

var host = builder.Build();
await host.RunAsync();