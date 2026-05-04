namespace WeatherStation.BusinessLogic.Contracts;

using WeatherStation.BusinessLogic.DTOs;

public interface ICityService
{
    Task<IEnumerable<CityDto>> GetAllCitiesAsync();
    Task<CityDto?> GetCityByIdAsync(int id);
    Task<CityDto> CreateCityAsync(CreateCityDto createDto);
    Task<bool> UpdateCityAsync(int id, CreateCityDto updateDto);
    Task<bool> DeleteCityAsync(int id);
    Task<CityDto> AddCityAsync(CityDto cityDto);
}
