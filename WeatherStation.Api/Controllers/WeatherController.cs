using Microsoft.AspNetCore.Mvc;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.DataAccess.Entities;

namespace WeatherStation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherRecord>>> GetAll()
    {
        var records = await _weatherService.GetAllRecordsAsync();
        return Ok(records);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WeatherRecord>> GetById(int id)
    {
        var record = await _weatherService.GetRecordByIdAsync(id);
        if (record == null)
            return NotFound();

        return Ok(record);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] WeatherRecord record)
    {
        await _weatherService.CreateRecordAsync(record);
        return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] WeatherRecord record)
    {
        if (id != record.Id)
            return BadRequest("ID у шляху не співпадає з ID у тілі запиту.");

        var existingRecord = await _weatherService.GetRecordByIdAsync(id);
        if (existingRecord == null)
            return NotFound();

        await _weatherService.UpdateRecordAsync(record);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _weatherService.DeleteRecordAsync(id);
        return NoContent();
    }
}