namespace WeatherStation.BusinessLogic.DTOs;

public class WeatherRecordDto
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double WindSpeed { get; set; }
    public DateTime RecordedAt { get; set; }
}
