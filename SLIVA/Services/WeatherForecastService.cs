using Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Services.Abstractions;

namespace Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly MyDbContext _dbContext;

    public WeatherForecastService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultDto> ChangeWeatherForecast(ChangeWeatherForecastDto dto)
    {
        var weatherForecast = await _dbContext.Weathers.FirstOrDefaultAsync(w => w.WeatherForecastId == dto.Id);
        if (weatherForecast == null)
        {
            return new ResultDto($"Weather forecast with id = {dto.Id} doesn't exist");
        }

        var authorDto = GetAuthorInfo(dto.Author);

        if (!authorDto.IsSuccess)
            return new ResultDto(authorDto.ErrorMessage);
        
        var author = await _dbContext.Authors
            .FirstOrDefaultAsync(a => a.FirstName == authorDto.Firstname && a.LastName == authorDto.Lastname);

        if (author == null)
        {
            author = new Author() {LastName = authorDto.Lastname, FirstName = authorDto.Firstname};
            await _dbContext.Authors.AddAsync(author);
        }
        
        var changedWeatherForecast = new WeatherForecast()
        {
            WeatherForecastId = weatherForecast.WeatherForecastId,
            AuthorId = dto.Id,
            Author = author,
            City = dto.City,
            CreatingDate = dto.CreatingDate,
            Date = dto.Date,
            Summary = dto.Summary,
            TemperatureC = dto.TemperatureC
        };
        
        _dbContext.Weathers.Remove(weatherForecast);
        await _dbContext.Weathers.AddAsync(changedWeatherForecast);
        await _dbContext.SaveChangesAsync();
        return new ResultDto();
    }

    public async Task<ResultDto> CreateWeatherForecast(CreateWeatherForecastDto dto)
    {
        var authorInfo = GetAuthorInfo(dto.Author);
        if (!authorInfo.IsSuccess)
            return new ResultDto(authorInfo.ErrorMessage);

        var author = await _dbContext.Authors
            .FirstOrDefaultAsync(a => a.FirstName == authorInfo.Firstname && a.LastName == authorInfo.Lastname);
        if (author == null)
            author = new Author() { LastName = authorInfo.Lastname, FirstName = authorInfo.Firstname};
        
        var weatherForecast = new WeatherForecast()
        {
            Date = dto.Date,
            Author = author,
            City = dto.City,
            CreatingDate = dto.CreatingDate,
            Summary = dto.Summary,
        };
        await _dbContext.Weathers.AddAsync(weatherForecast);
        await _dbContext.SaveChangesAsync();
        return new ResultDto();
    }

    public async Task<ResultDto> DeleteWeatherForecast(DeleteWeatherForecastDto dto)
    {
        var rowsChanged = await _dbContext.Weathers.Where(w => w.WeatherForecastId == dto.Id).ExecuteDeleteAsync();
        if (rowsChanged == 0)
        {
            return new ResultDto($"Weather forecast with id = {dto.Id} doesn't exist");
        }

        return new ResultDto();
    }

    public async Task<WeatherForecastInfoDto> GetWeatherForecastWithPagination(PaginationDto dto)
    {
        var weatherForecasts = _dbContext.Weathers
            .AsNoTracking()
            .Where(w => w.Date <= dto.EndDate && w.Date >= dto.StartDate)
            .OrderBy(w => w.WeatherForecastId)
            .Skip((dto.Page - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .Include(w => w.Author)
            .AsEnumerable()
            .Select(w => new WeatherForecastDto(w.WeatherForecastId, w.Date, w.CreatingDate,
                w.TemperatureC, w.TemperatureF,
                w.City, w?.Summary!, new AuthorDto(w.AuthorId,w?.Author.FirstName!, w?.Author.LastName!)));

        var weatherForecastInfo = new WeatherForecastInfoDto(weatherForecasts);
        return weatherForecastInfo;
    }
    
    private AuthorInfoDto GetAuthorInfo(string dto)
    {
        var authorInfo = dto.Split(" ");
        if (authorInfo.Length != 2)
            return new AuthorInfoDto("Invalid author info!");
            
        var firstname = authorInfo[0];
        var lastname = authorInfo[1];
        return new AuthorInfoDto(firstname, lastname);
    }
}