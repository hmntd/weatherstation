using Microsoft.EntityFrameworkCore;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.DataAccess.Data;
using WeatherStation.DataAccess.Entities;

namespace WeatherStation.DataAccess.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly WeatherStationDbContext _context;

    public WeatherRepository(WeatherStationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherRecord>> GetAllAsync() =>
        await _context.WeatherRecords.ToListAsync();

    public async Task<WeatherRecord?> GetByIdAsync(int id) =>
        await _context.WeatherRecords.FindAsync(id);

    public async Task AddAsync(WeatherRecord record)
    {
        await _context.WeatherRecords.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WeatherRecord record)
    {
        _context.WeatherRecords.Update(record);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var record = await _context.WeatherRecords.FindAsync(id);
        if (record != null)
        {
            _context.WeatherRecords.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}