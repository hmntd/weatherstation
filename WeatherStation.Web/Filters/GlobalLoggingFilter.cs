namespace WeatherStation.Web.Filters;

using Microsoft.AspNetCore.Mvc.Filters;

public class GlobalLoggingFilter : IActionFilter
{
    private readonly ILogger<GlobalLoggingFilter> _logger;

    public GlobalLoggingFilter(ILogger<GlobalLoggingFilter> logger) => _logger = logger;

    public void OnActionExecuting(ActionExecutingContext context)
        => _logger.LogInformation("  >>> 2. [GLOBAL FILTER] OnActionExecuting");

    public void OnActionExecuted(ActionExecutedContext context)
        => _logger.LogInformation("  <<< 7. [GLOBAL FILTER] OnActionExecuted");
}