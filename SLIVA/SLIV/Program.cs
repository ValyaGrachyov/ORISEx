using Microsoft.EntityFrameworkCore;
using Persistance;
using Services;
using Services.Abstractions;
using SLIV.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = new ConnectionStringOptions();
builder.Configuration.GetSection(ConnectionStringOptions.ConnectionString).Bind(cs);

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(cs.Connection_String));

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IFakeService, FakeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
