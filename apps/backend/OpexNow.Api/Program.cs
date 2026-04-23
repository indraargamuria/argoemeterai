// (Arga) 2026-04-24 - Added for Link to Db Context
using Microsoft.EntityFrameworkCore;
using OpexNow.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// (Arga) 2026-04-24 - Added for Link to Controller
builder.Services.AddControllers();

// (Arga) 2026-04-24 - Added for Link to Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// (Arga) 2026-04-24 - Added for Link to Db Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
      builder.Configuration.GetConnectionString("DefaultConnection")
));

var app = builder.Build();

// (Arga) 2026-04-24 - Add swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
