using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Application.Services;
using CarRentalService.Infrastructure.InMemory.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IRenterRepository, RenterRepository>();
builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
builder.Services.AddSingleton<IVehicleModelRepository, VehicleModelRepository>();
builder.Services.AddSingleton<IModelGenerationRepository, ModelGenerationRepository>();
builder.Services.AddSingleton<IRentalRepository, RentalRepository>();

builder.Services.AddScoped<IRenterService, RenterService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IModelGenerationService, ModelGenerationService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();