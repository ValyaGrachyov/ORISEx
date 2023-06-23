namespace Contracts;

public class WeatherForecastResDto
{
    public WeatherForecastDto WeatherForecastDto { get; set; }
    public string ErrorMessage { get; set; }
    public bool IsSuccess { get; set; }

    public WeatherForecastResDto(string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccess = false;
    }

    public WeatherForecastResDto(WeatherForecastDto dto)
    {
        WeatherForecastDto = dto;
        IsSuccess = true;
    }
}