namespace WeatherStation.DataAccess.Repositories;

using Microsoft.EntityFrameworkCore;
using WeatherStation.DataAccess.Data;
using WeatherStation.DataAccess.Contracts;
using WeatherStation.DataAccess.Entities;

public class UserRepository : IUserRepository
{
    private readonly WeatherStationDbContext _context;

    public UserRepository(WeatherStationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }
}