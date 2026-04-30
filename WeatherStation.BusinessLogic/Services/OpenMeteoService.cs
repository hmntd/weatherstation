namespace WeatherStation.BusinessLogic.Services;

using System.Globalization;
using System.Text.Json;
using WeatherStation.BusinessLogic.DTOs;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.BusinessLogic.OpenMeteo;

public class OpenMeteoService : IWeatherProviderService
{
    private readonly HttpClient _httpClient;
    public OpenMeteoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<WeatherRecordDto>> FetchWeatherFromApiAsync(int cityId, double latitude, double longitude)
    {
        var lat = latitude.ToString(CultureInfo.InvariantCulture);
        var lon = longitude.ToString(CultureInfo.InvariantCulture);

        var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m&past_days=1&forecast_days=3&timezone=auto";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var weatherData = JsonSerializer.Deserialize<OpenMeteoResponse>(content);

        if (weatherData?.Hourly == null) return Enumerable.Empty<WeatherRecordDto>();

        var records = new List<WeatherRecordDto>();
        var hourly = weatherData.Hourly;

        for (int i = 0; i < hourly.Time.Count; i++)
        {
            if (i % 3 != 0) continue;

            records.Add(new WeatherRecordDto
            {
                CityId = cityId,
                RecordedAt = DateTime.Parse(hourly.Time[i]),
                Temperature = hourly.Temperature[i],
                Humidity = hourly.Humidity[i],
                WindSpeed = hourly.WindSpeed[i]
            });
        }

        return records;
    }
}