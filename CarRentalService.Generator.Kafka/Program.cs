using CarRentalService.Application.Contracts.Rental;
using CarRentalService.Generator.Kafka;
using CarRentalService.Generator.Kafka.Serializers;
using CarRentalService.Generator.Kafka.Services;
using CarRentalService.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddKafkaProducer<Guid, IList<RentalRequest>>("car-rental-kafka",
    configureBuilder: kafkaBuilder =>
    {
        kafkaBuilder.SetKeySerializer(new KeySerializer());
        kafkaBuilder.SetValueSerializer(new ValueSerializer());
    });

builder.Services.AddSingleton<IProducerService, GeneratorService>();
builder.Services.AddHostedService<KafkaProducerService>();

var host = builder.Build();
await host.RunAsync();