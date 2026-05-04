using Microsoft.EntityFrameworkCore;
using WeatherStation.DataAccess.Entities;

namespace WeatherStation.DataAccess.Data;

public class WeatherStationDbContext : DbContext
{
    public WeatherStationDbContext(DbContextOptions<WeatherStationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<WeatherRecord> WeatherRecords => Set<WeatherRecord>();
    public DbSet<AccessToken> AccessTokens => Set<AccessToken>();
    public DbSet<UserCity> UserCities => Set<UserCity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserCity>()
            .HasKey(uc => new { uc.UserId, uc.CityId });

        modelBuilder.Entity<UserCity>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCities)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCity>()
            .HasOne(uc => uc.City)
            .WithMany(c => c.UserCities)
            .HasForeignKey(uc => uc.CityId);

        modelBuilder.Entity<WeatherRecord>()
            .HasOne(wr => wr.City)
            .WithMany(c => c.WeatherRecords)
            .HasForeignKey(wr => wr.CityId);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
