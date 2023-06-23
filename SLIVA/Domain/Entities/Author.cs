﻿namespace Domain.Entities;

public class Author
{
    public int AuthorId { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public List<WeatherForecast> WeatherForecasts { get; set; }
}