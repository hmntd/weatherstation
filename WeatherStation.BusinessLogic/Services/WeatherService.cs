using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.DataAccess.Entities;

namespace WeatherStation.BusinessLogic.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _weatherRepository;

    public WeatherService(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public Task<IEnumerable<WeatherRecord>> GetAllRecordsAsync()
    {
        return _weatherRepository.GetAllAsync();
    }

    public Task<WeatherRecord?> GetRecordByIdAsync(int id)
    {
        return _weatherRepository.GetByIdAsync(id);
    }

    public Task CreateRecordAsync(WeatherRecord record)
    {
        // Тут можна додати бізнес-логіку, наприклад, валідацію перед збереженням
        return _weatherRepository.AddAsync(record);
    }

    public Task UpdateRecordAsync(WeatherRecord record)
    {
        return _weatherRepository.UpdateAsync(record);
    }

    public Task DeleteRecordAsync(int id)
    {
        return _weatherRepository.DeleteAsync(id);
    }
}