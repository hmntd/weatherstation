namespace WeatherStation.DataAccess.Entities;

public class City : BaseEntity
{
    public string Name { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Навігаційна властивість: Одне місто має багато записів про погоду (історія)
    public ICollection<WeatherRecord> WeatherRecords { get; set; } = new List<WeatherRecord>();
    
    // Навігаційна властивість: Зв'язок Many-to-Many з користувачами
    public ICollection<UserCity> UserCities { get; set; } = new List<UserCity>();
}
