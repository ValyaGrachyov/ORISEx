namespace Contracts;

public record ChangeWeatherForecastDto(int Id, 
    DateTime Date, 
    DateTime CreatingDate, 
    int TemperatureC,
    string City, 
    string Summary, 
    string Author);
