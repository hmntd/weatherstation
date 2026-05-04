namespace WeatherStation.BusinessLogic.Services;

using WeatherStation.BusinessLogic.DTOs;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.BusinessLogic.Services;
using WeatherStation.DataAccess.Entities;
using WeatherStation.DataAccess.Repositories;
using WeatherStation.DataAccess.Contracts;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<IEnumerable<CityDto>> GetAllCitiesAsync()
    {
        var cities = await _cityRepository.GetAllAsync();
        // Маппинг з Entity в DTO (можна використовувати AutoMapper, але для початку зробимо вручну)
        return cities.Select(c => new CityDto
        {
            Id = c.Id,
            Name = c.Name,
            Latitude = c.Latitude,
            Longitude = c.Longitude
        });
    }

    public async Task<CityDto?> GetCityByIdAsync(int id)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if (city == null) return null;

        return new CityDto { Id = city.Id, Name = city.Name, Latitude = city.Latitude, Longitude = city.Longitude };
    }

    public async Task<CityDto> CreateCityAsync(CreateCityDto createDto)
    {
        var city = new City
        {
            Name = createDto.Name,
            Latitude = createDto.Latitude,
            Longitude = createDto.Longitude
        };

        var createdCity = await _cityRepository.AddAsync(city);

        return new CityDto { Id = createdCity.Id, Name = createdCity.Name, Latitude = createdCity.Latitude, Longitude = createdCity.Longitude };
    }

    public async Task<bool> UpdateCityAsync(int id, CreateCityDto updateDto)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if (city == null) return false;

        city.Name = updateDto.Name;
        city.Latitude = updateDto.Latitude;
        city.Longitude = updateDto.Longitude;

        await _cityRepository.UpdateAsync(city);
        return true;
    }

    public async Task<bool> DeleteCityAsync(int id)
    {
        var city = await _cityRepository.GetByIdAsync(id);
        if (city == null) return false;

        await _cityRepository.DeleteAsync(city);
        return true;
    }
}
