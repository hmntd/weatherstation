namespace WeatherStation.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherStation.BusinessLogic.DTOs;
using WeatherStation.BusinessLogic.Contracts;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cities = await _cityService.GetAllCitiesAsync();
        return Ok(cities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        if (city == null) return NotFound($"City with ID {id} not found.");

        return Ok(city);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCityDto createDto)
    {
        var newCity = await _cityService.CreateCityAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = newCity.Id }, newCity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCityDto updateDto)
    {
        var updated = await _cityService.UpdateCityAsync(id, updateDto);
        if (!updated) return NotFound($"City with ID {id} not found.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _cityService.DeleteCityAsync(id);
        if (!deleted) return NotFound($"City with ID {id} not found.");

        return NoContent();
    }
}
