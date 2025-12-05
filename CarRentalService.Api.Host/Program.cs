using CarRentalService.Application;
using CarRentalService.Infrastructure;
using CarRentalService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using CarRentalService.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Car Rental Service API",
        Version = "v1"
    });
});

var app = builder.Build();
app.MapDefaultEndpoints();

try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<CarRentalDbContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Database error: {ex.Message}");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();