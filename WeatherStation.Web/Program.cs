using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.BusinessLogic.Services;
using WeatherStation.DataAccess.Data;
using WeatherStation.DataAccess.Contracts;
using WeatherStation.DataAccess.Repositories;
using WeatherStation.Web.Filters;
using WeatherStation.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/app-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalLoggingFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WeatherStationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddScoped<IWeatherRecordRepository, WeatherRecordRepository>();
builder.Services.AddScoped<IWeatherRecordService, WeatherRecordService>();
builder.Services.AddHttpClient<IWeatherProviderService, OpenMeteoService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ControllerExecutionFilter>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                {
                    context.Token = context.Request.Cookies["X-Access-Token"];
                }
                return Task.CompletedTask;
            }
        };

        options.Events.OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.Redirect("/Auth/Login");
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<CustomLoggingMiddleware>();

app.Use(async (context, next) =>
{
    await next.Invoke();
});

app.Map("/view-logs", async context =>
{
    context.Response.ContentType = "text/plain; charset=utf-8";
    var today = DateTime.Now.ToString("yyyyMMdd");
    var logFilePath = $"logs/app-log-{today}.txt";

    if (File.Exists(logFilePath))
    {
        var logs = await File.ReadAllTextAsync(logFilePath);
        await context.Response.WriteAsync(logs);
    }
    else
    {
        await context.Response.WriteAsync("Файл логів ще не створено або він порожній.");
    }
});

app.Map("/lab4-terminal", mappedApp =>
{
    mappedApp.Run(async context =>
    {
        await context.Response.WriteAsync("Термінальна відповідь.");
    });
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=City}/{action=Index}/{id?}");

app.Run();