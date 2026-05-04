namespace WeatherStation.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherStation.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class WeatherRecordController : Controller
{
    private readonly IWeatherRecordService _weatherRecordService;

    public WeatherRecordController(IWeatherRecordService weatherRecordService)
    {
        _weatherRecordService = weatherRecordService;
    }

    // GET: /WeatherRecord/History?cityId=1
    public async Task<IActionResult> History(int cityId)
    {
        var records = await _weatherRecordService.GetHistoryForCityAsync(cityId);
        ViewBag.CityId = cityId;
        return View(records);
    }
}
