namespace WeatherStation.BusinessLogic.Contracts;

using WeatherStation.BusinessLogic.DTOs;

public interface IWeatherRecordService
{
    Task<IEnumerable<WeatherRecordDto>> GetHistoryForCityAsync(int cityId);
    Task<IEnumerable<WeatherRecordDto>> SyncWeatherForCityAsync(int cityId);
}
