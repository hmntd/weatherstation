namespace WeatherStation.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
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

    // GET: /Auth/ForgotPassword
    [HttpGet]
    public IActionResult ForgotPassword() => View();

    // POST: /Auth/ForgotPassword
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email, [FromServices] UserManager<IdentityUser> userManager)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action("ResetPassword", "Auth", new { email = email, token = token }, Request.Scheme);

            Console.WriteLine($"\n--- EMAIL SIMULATION ---\nTo: {email}\nLink: {resetLink}\n------------------------\n");

            ViewBag.Message = "Посилання для відновлення надіслано на вашу пошту (див. консоль).";
        }
        else
        {
            ViewBag.Message = "Користувача з таким Email не знайдено.";
        }
        return View();
    }

    // GET: /Auth/ResetPassword
    [HttpGet]
    public IActionResult ResetPassword(string email, string token)
    {
        return View(new { Email = email, Token = token });
    }

    // POST: /Auth/ResetPassword
    [HttpPost]
    public async Task<IActionResult> ResetPassword(string email, string token, string newPassword, [FromServices] UserManager<IdentityUser> userManager)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
        }
        ViewBag.Error = "Помилка відновлення пароля (недійсний токен).";
        return View(new { Email = email, Token = token });
    }
}
