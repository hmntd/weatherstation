namespace WeatherStation.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherStation.BusinessLogic.Contracts;
using WeatherStation.BusinessLogic.DTOs;
using WeatherStation.BusinessLogic.DTOs.AuthDTOs;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // GET: /Auth/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Auth/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            var response = await _authService.LoginAsync(model);

            SetTokenCookie(response.Token);

            return RedirectToAction("Index", "City");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Неправильний email або пароль.");
            return View(model);
        }
    }

    // GET: /Auth/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Auth/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterDto model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            var response = await _authService.RegisterAsync(model);

            SetTokenCookie(response.Token);

            return RedirectToAction("Index", "City");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Помилка реєстрації. Можливо, такий Email вже існує.");
            return View(model);
        }
    }

    // POST: /Auth/Logout
    [HttpPost]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("X-Access-Token");
        return RedirectToAction("Login");
    }

    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(1),
            SameSite = SameSiteMode.Strict
        };
        Response.Cookies.Append("X-Access-Token", token, cookieOptions);
    }
}
