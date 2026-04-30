namespace WeatherStation.BusinessLogic.Contracts;

using WeatherStation.BusinessLogic.DTOs;

public interface IWeatherProviderService
{
    Task<IEnumerable<WeatherRecordDto>> FetchWeatherFromApiAsync(int cityId, double latitude, double longitude);
}
