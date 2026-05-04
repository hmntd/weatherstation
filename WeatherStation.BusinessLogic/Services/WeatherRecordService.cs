namespace WeatherStation.BusinessLogic.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherStation.BusinessLogic.DTOs;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.DataAccess.Entities;
using WeatherStation.DataAccess.Contracts;
using WeatherStation.DataAccess.Repositories;

public class WeatherRecordService : IWeatherRecordService
{
    private readonly IWeatherRecordRepository _recordRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IWeatherProviderService _weatherProvider;

    public WeatherRecordService(
        IWeatherRecordRepository recordRepository,
        ICityRepository cityRepository,
        IWeatherProviderService weatherProvider)
    {
        _recordRepository = recordRepository;
        _cityRepository = cityRepository;
        _weatherProvider = weatherProvider;
    }

    public async Task<IEnumerable<WeatherRecordDto>> GetHistoryForCityAsync(int cityId)
    {
        var records = await _recordRepository.GetByCityIdAsync(cityId);
        return records.Select(r => new WeatherRecordDto
        {
            Id = r.Id,
            CityId = r.CityId,
            Temperature = r.Temperature,
            Humidity = r.Humidity,
            WindSpeed = r.WindSpeed,
            RecordedAt = r.RecordedAt
        });
    }

    public async Task<IEnumerable<WeatherRecordDto>> SyncWeatherForCityAsync(int cityId)
    {
        var city = await _cityRepository.GetByIdAsync(cityId);
        if (city == null) throw new Exception("City not found");

        var newRecordsDto = await _weatherProvider.FetchWeatherFromApiAsync(cityId, city.Latitude, city.Longitude);

        var dtoList = newRecordsDto.ToList();
        if (!dtoList.Any()) return dtoList;

        await _recordRepository.DeleteOldRecordsAsync(cityId, DateTime.UtcNow.AddDays(-2));

        var entities = dtoList.Select(dto => new WeatherRecord
        {
            CityId = dto.CityId,
            Temperature = dto.Temperature,
            Humidity = dto.Humidity,
            WindSpeed = dto.WindSpeed,
            RecordedAt = dto.RecordedAt
        });

        await _recordRepository.AddRangeAsync(entities);

        return dtoList;
    }
}
