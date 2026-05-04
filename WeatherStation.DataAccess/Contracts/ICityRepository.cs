namespace WeatherStation.DataAccess.Contracts;

using WeatherStation.DataAccess.Entities;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<City?> GetByIdAsync(int id);
    Task<City> AddAsync(City city);
    Task UpdateAsync(City city);
    Task DeleteAsync(City city);
}
