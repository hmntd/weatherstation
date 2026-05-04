namespace WeatherStation.DataAccess.Contracts;

using WeatherStation.DataAccess.Entities;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
}
