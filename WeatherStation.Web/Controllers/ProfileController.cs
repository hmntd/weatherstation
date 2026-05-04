namespace WeatherStation.Web.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(string userName)
    {
        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user != null && !string.IsNullOrWhiteSpace(userName))
        {
            user.UserName = userName;
            await _userManager.UpdateAsync(user);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (user != null)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Помилка зміни пароля. Перевірте старий пароль.";
            }
            else
            {
                TempData["Success"] = "Пароль успішно змінено!";
            }
        }
        return RedirectToAction("Index");
    }
}