namespace WeatherStation.Web.Filters;

using Microsoft.AspNetCore.Mvc.Filters;

public class ControllerExecutionFilter : IActionFilter
{
    private readonly ILogger<ControllerExecutionFilter> _logger;

    public ControllerExecutionFilter(ILogger<ControllerExecutionFilter> logger) => _logger = logger;

    public void OnActionExecuting(ActionExecutingContext context)
        => _logger.LogInformation("    >>> 3. [CONTROLLER FILTER (ServiceFilter)] OnActionExecuting");

    public void OnActionExecuted(ActionExecutedContext context)
        => _logger.LogInformation("    <<< 6. [CONTROLLER FILTER (ServiceFilter)] OnActionExecuted");
}