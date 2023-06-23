using Contracts;

namespace Services.Abstractions;

public interface IFakeService
{
    Task<WeatherForecastResDto> GetActualInfoAsync(int id);
}