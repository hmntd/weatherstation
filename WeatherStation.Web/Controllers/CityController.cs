namespace WeatherStation.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.BusinessLogic.DTOs;

public class CityController : Controller
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    // GET: /City/Index
    public async Task<IActionResult> Index()
    {
        var cities = await _cityService.GetAllCitiesAsync();

        return View(cities);
    }

    // GET: /City/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /City/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CityDto cityDto)
    {
        if (ModelState.IsValid)
        {
            await _cityService.AddCityAsync(cityDto);
            return RedirectToAction(nameof(Index));
        }

        return View(cityDto);
    }
}
