namespace WeatherStation.Web.Filters;

using Microsoft.AspNetCore.Mvc.Filters;

public class ActionTimingFilter : IActionFilter
{
    private readonly ILogger<ActionTimingFilter> _logger;
    private readonly string _customMessage;

    public ActionTimingFilter(ILogger<ActionTimingFilter> logger, string customMessage)
    {
        _logger = logger;
        _customMessage = customMessage;
    }

    public void OnActionExecuting(ActionExecutingContext context)
        => _logger.LogInformation("      >>> 4. [ACTION FILTER (TypeFilter)] Повідомлення: {Msg}", _customMessage);

    public void OnActionExecuted(ActionExecutedContext context)
        => _logger.LogInformation("      <<< 5. [ACTION FILTER (TypeFilter)] Завершено");
}