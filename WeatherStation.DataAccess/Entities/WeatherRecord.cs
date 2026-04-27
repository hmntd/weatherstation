namespace WeatherStation.DataAccess.Entities;

public class WeatherRecord : BaseEntity
{
    public string City { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double WindSpeed { get; set; }
    public DateTime MeasurementTime { get; set; }
}
