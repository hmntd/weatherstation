namespace WeatherStation.DataAccess.Repositories;

using WeatherStation.DataAccess.Contracts;
using WeatherStation.DataAccess.Entities;
using WeatherStation.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

public class WeatherRecordRepository : IWeatherRecordRepository
{
    private readonly WeatherStationDbContext _context;

    public WeatherRecordRepository(WeatherStationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherRecord>> GetByCityIdAsync(int cityId)
    {
        return await _context.WeatherRecords
            .Where(wr => wr.CityId == cityId)
            .OrderBy(wr => wr.RecordedAt)
            .ToListAsync();
    }

    public async Task AddRangeAsync(IEnumerable<WeatherRecord> records)
    {
        await _context.WeatherRecords.AddRangeAsync(records);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOldRecordsAsync(int cityId, DateTime beforeDate)
    {
        var oldRecords = await _context.WeatherRecords
            .Where(wr => wr.CityId == cityId && wr.RecordedAt < beforeDate)
            .ToListAsync();

        if (oldRecords.Any())
        {
            _context.WeatherRecords.RemoveRange(oldRecords);
            await _context.SaveChangesAsync();
        }
    }
}
