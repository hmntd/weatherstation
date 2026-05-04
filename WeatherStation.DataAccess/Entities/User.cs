namespace WeatherStation.DataAccess.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<AccessToken> AccessTokens { get; set; } = new List<AccessToken>();

    // Навігаційна властивість: Зв'язок Many-to-Many з містами (улюблені локації)
    public ICollection<UserCity> UserCities { get; set; } = new List<UserCity>();
}
