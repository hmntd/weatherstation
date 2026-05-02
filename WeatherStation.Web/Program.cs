using Microsoft.EntityFrameworkCore;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.BusinessLogic.Services;
using WeatherStation.DataAccess.Data;
using WeatherStation.DataAccess.Contracts;
using WeatherStation.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WeatherStationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddScoped<IWeatherRecordRepository, WeatherRecordRepository>();
builder.Services.AddScoped<IWeatherRecordService, WeatherRecordService>();
builder.Services.AddHttpClient<IWeatherProviderService, OpenMeteoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=City}/{action=Index}/{id?}");

app.Run();
