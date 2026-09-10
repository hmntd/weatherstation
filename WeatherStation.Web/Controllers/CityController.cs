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

    // GET: /City/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        if (city == null) return NotFound("Місто не знайдено.");

        var model = new CreateCityDto
        {
            Name = city.Name,
            Latitude = city.Latitude,
            Longitude = city.Longitude
        };

        return View(model);
    }

    // POST: /City/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateCityDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var updated = await _cityService.UpdateCityAsync(id, model);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Сталася помилка при збереженні змін.");
            return View(model);
        }
    }
}