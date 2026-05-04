namespace WeatherStation.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.BusinessLogic.DTOs;

[Authorize]
public class CityController : Controller
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    // GET: /City
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cities = await _cityService.GetAllCitiesAsync();
        return View(cities);
    }

    // GET: /City/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /City/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCityDto model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            await _cityService.CreateCityAsync(model);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Помилка при додаванні міста. Можливо, воно вже існує.");
            return View(model);
        }
    }
}