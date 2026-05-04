using System.Text.Json.Serialization;

namespace WeatherStation.BusinessLogic.OpenMeteo;

public class OpenMeteoResponse
{
    [JsonPropertyName("hourly")]
    public HourlyData Hourly { get; set; } = null!;
}

public class HourlyData
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = new();

    [JsonPropertyName("temperature_2m")]
    public List<double> Temperature { get; set; } = new();

    [JsonPropertyName("relative_humidity_2m")]
    public List<double> Humidity { get; set; } = new();

    [JsonPropertyName("wind_speed_10m")]
    public List<double> WindSpeed { get; set; } = new();
}