namespace WeatherStation.BusinessLogic.Contracts;

using WeatherStation.DataAccess.Entities;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherRecord>> GetAllAsync();
    Task<WeatherRecord?> GetByIdAsync(int id);
    Task AddAsync(WeatherRecord record);
    Task UpdateAsync(WeatherRecord record);
    Task DeleteAsync(int id);
}
