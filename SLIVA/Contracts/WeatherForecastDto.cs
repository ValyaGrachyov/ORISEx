namespace Contracts;

public record WeatherForecastDto(int Id, 
    DateTime Date, 
    DateTime CreatingDate, 
    int TemperatureC,
    int TemperatureF,
    string City, 
    string Summary, 
    AuthorDto Author);