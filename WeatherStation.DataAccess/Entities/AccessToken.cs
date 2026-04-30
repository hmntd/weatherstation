namespace WeatherStation.DataAccess.Entities;

public class AccessToken : BaseEntity
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }

    // Зовнішній ключ (Foreign Key)
    public int UserId { get; set; }

    // Навігаційна властивість (посилання на власника токена)
    public User User { get; set; } = null!;
}
