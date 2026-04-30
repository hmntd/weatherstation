namespace WeatherStation.DataAccess.Entities;

public class WeatherRecord : BaseEntity
{
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double WindSpeed { get; set; }
    public DateTime RecordedAt { get; set; }

    // Зовнішній ключ (Foreign Key) на таблицю Cities
    public int CityId { get; set; }

    // Навігаційна властивість
    public City City { get; set; } = null!;
}
