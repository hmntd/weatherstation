namespace WeatherStation.DataAccess.Entities;

public class UserCity
{
    // Композитний ключ буде налаштовано в DbContext (UserId + CityId)
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int CityId { get; set; }
    public City City { get; set; } = null!;
}