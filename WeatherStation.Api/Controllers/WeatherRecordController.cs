namespace WeatherStation.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherStation.BusinessLogic.Contracts;

[ApiController]
[Route("api/[controller]")]
public class WeatherRecordController : ControllerBase
{
    private readonly IWeatherRecordService _weatherRecordService;

    public WeatherRecordController(IWeatherRecordService weatherRecordService)
    {
        _weatherRecordService = weatherRecordService;
    }

    // GET: api/WeatherRecord/5/history
    [HttpGet("{cityId}/history")]
    public async Task<IActionResult> GetHistory(int cityId)
    {
        var records = await _weatherRecordService.GetHistoryForCityAsync(cityId);
        return Ok(records);
    }

    // POST: api/WeatherRecord/5/sync
    [HttpPost("{cityId}/sync")]
    public async Task<IActionResult> SyncWeather(int cityId)
    {
        try
        {
            var newRecords = await _weatherRecordService.SyncWeatherForCityAsync(cityId);

            if (!newRecords.Any())
            {
                return NotFound("Не вдалося отримати дані з Open Meteo.");
            }

            return Ok(newRecords);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
