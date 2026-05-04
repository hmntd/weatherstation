namespace WeatherStation.DataAccess.Contracts;

using WeatherStation.DataAccess.Entities;

public interface IWeatherRecordRepository
{
    Task<IEnumerable<WeatherRecord>> GetByCityIdAsync(int cityId);

    Task AddRangeAsync(IEnumerable<WeatherRecord> records);

    Task DeleteOldRecordsAsync(int cityId, DateTime beforeDate);
}