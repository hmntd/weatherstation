namespace WeatherStation.Api.HostedServices;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherStation.BusinessLogic.Contracts;

public class WeatherSyncBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WeatherSyncBackgroundService> _logger;

    public WeatherSyncBackgroundService(IServiceProvider serviceProvider, ILogger<WeatherSyncBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Фоновий сервіс синхронізації погоди запущено.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextMidnight = now.Date.AddDays(1);
            var timeToWait = nextMidnight - now;

            _logger.LogInformation($"Наступна синхронізація через: {timeToWait.Hours} год {timeToWait.Minutes} хв.");

            await Task.Delay(timeToWait, stoppingToken);

            if (stoppingToken.IsCancellationRequested) break;

            _logger.LogInformation("Починаємо щоденну синхронізацію погоди...");

            using (var scope = _serviceProvider.CreateScope())
            {
                var cityService = scope.ServiceProvider.GetRequiredService<ICityService>();
                var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherRecordService>();

                try
                {
                    var cities = await cityService.GetAllCitiesAsync();
                    foreach (var city in cities)
                    {
                        await weatherService.SyncWeatherForCityAsync(city.Id);
                        _logger.LogInformation($"Погоду для міста {city.Name} оновлено.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Помилка під час масової синхронізації.");
                }
            }
        }
    }
}
