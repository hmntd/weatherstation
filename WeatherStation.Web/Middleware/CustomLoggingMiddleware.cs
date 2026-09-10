namespace WeatherStation.Web.Middleware;

public class CustomLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomLoggingMiddleware> _logger;

    public CustomLoggingMiddleware(RequestDelegate next, ILogger<CustomLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(">>> [MIDDLEWARE] Вхідний запит: {Path} о {Time}",
            context.Request.Path, DateTime.Now.ToLongTimeString());

        await _next(context);

        _logger.LogInformation("<<< [MIDDLEWARE] Вихід: {Status}", context.Response.StatusCode);
    }
}