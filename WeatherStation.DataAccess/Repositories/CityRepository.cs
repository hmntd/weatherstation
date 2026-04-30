namespace WeatherStation.DataAccess.Repositories;

using Microsoft.EntityFrameworkCore;
using WeatherStation.DataAccess.Data;
using WeatherStation.DataAccess.Entities;
using WeatherStation.DataAccess.Contracts;

public class CityRepository : ICityRepository
{
    private readonly WeatherStationDbContext _context;

    public CityRepository(WeatherStationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.Cities.ToListAsync();
    }

    public async Task<City?> GetByIdAsync(int id)
    {
        return await _context.Cities.FindAsync(id);
    }

    public async Task<City> AddAsync(City city)
    {
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();
        return city;
    }

    public async Task UpdateAsync(City city)
    {
        _context.Cities.Update(city);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(City city)
    {
        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
    }
}
