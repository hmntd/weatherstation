using WeatherStation.DataAccess.Entities;

namespace WeatherStation.BusinessLogic.Contracts;

public interface IWeatherService
{
    Task<IEnumerable<WeatherRecord>> GetAllRecordsAsync();
    Task<WeatherRecord?> GetRecordByIdAsync(int id);
    Task CreateRecordAsync(WeatherRecord record);
    Task UpdateRecordAsync(WeatherRecord record);
    Task DeleteRecordAsync(int id);
}
