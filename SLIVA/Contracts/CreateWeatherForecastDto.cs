namespace Contracts;

public record CreateWeatherForecastDto(
    DateTime Date, 
    DateTime CreatingDate, 
    int TemperatureC,
    string City, 
    string Summary, 
    string Author);