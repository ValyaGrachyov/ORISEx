using Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Services.Abstractions;

namespace Services;

public class FakeService : IFakeService
{
    private readonly MyDbContext _dbContext;

    public FakeService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<WeatherForecastResDto> GetActualInfoAsync(int id)
    {
        var res = await _dbContext.Weathers.Where(w => w.WeatherForecastId == id)
            .Include(w => w.Author)
            .FirstOrDefaultAsync();

        if (res == null)
            return new WeatherForecastResDto("Forecast doesn't exists");

        var dto = new WeatherForecastDto(id, 
            res.Date, res.CreatingDate, res.TemperatureC, res.TemperatureF,
            res.City, res.Summary, new AuthorDto(res.AuthorId, res.Author.LastName,
                res.Author.FirstName));
        
        return new WeatherForecastResDto(dto);
    }
}