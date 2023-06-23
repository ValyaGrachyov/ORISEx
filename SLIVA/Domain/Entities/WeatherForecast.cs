using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class WeatherForecast
{
    public int WeatherForecastId { get; set; }

    public DateTime Date { get; set; }
    
    public string City { get; set; }
    
    public DateTime CreatingDate { get; set; }

    [Range(-100, 80)]
    public int TemperatureC { get; set; }

    public virtual int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
    
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}