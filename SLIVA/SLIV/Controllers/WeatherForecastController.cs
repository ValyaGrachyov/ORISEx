using Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Oris_examTask.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly IFakeService _fakeService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IWeatherForecastService weatherForecastService, IFakeService fakeService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
        _fakeService = fakeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWeatherForecast([FromQuery] DateTimeDto dto, [FromQuery] int page,  [FromQuery] int pageSize = 16)
    {
        var paginationDto = new PaginationDto(page, pageSize, dto.StartDate, dto.EndDate);
        var result = await _weatherForecastService.GetWeatherForecastWithPagination(paginationDto);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> ChangeWeatherForecast([FromBody] ChangeWeatherForecastDto dto)
    {
        var result = await _weatherForecastService.ChangeWeatherForecast(dto);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteWeatherForecast(int id)
    {
        var result = await _weatherForecastService.DeleteWeatherForecast(new DeleteWeatherForecastDto(id));
        if (result.IsSuccess)
            return NoContent();
        return BadRequest(result.ErrorMessage);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateWeatherForecast([FromBody] CreateWeatherForecastDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data");
        var result = await _weatherForecastService.CreateWeatherForecast(dto);
        if (result.IsSuccess)
            return NoContent();
        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("fake/{id:int}")]
    public async Task<IActionResult> GetInfoFromFakeService(int id)
    {
        return Ok(await _fakeService.GetActualInfoAsync(id));
    }
}
