using Microsoft.EntityFrameworkCore;
using WeatherStation.DataAccess.Entities;

namespace WeatherStation.DataAccess.Data;

public class WeatherStationDbContext : DbContext
{
    public WeatherStationDbContext(DbContextOptions<WeatherStationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeatherRecord> WeatherRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<WeatherRecord>()
            .Property(w => w.City)
            .IsRequired()
            .HasMaxLength(100);
    }
}