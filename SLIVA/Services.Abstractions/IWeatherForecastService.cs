using Contracts;

namespace Services.Abstractions;

public interface IWeatherForecastService
{
    Task<ResultDto> ChangeWeatherForecast(ChangeWeatherForecastDto dto);

    Task<ResultDto> CreateWeatherForecast(CreateWeatherForecastDto dto);

    Task<ResultDto> DeleteWeatherForecast(DeleteWeatherForecastDto dto);

    Task<WeatherForecastInfoDto> GetWeatherForecastWithPagination(PaginationDto dto);
}
