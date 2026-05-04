namespace WeatherStation.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();

        var userRoles = new Dictionary<string, IList<string>>();

        foreach (var user in users)
        {
            userRoles[user.Id] = await _userManager.GetRolesAsync(user);
        }

        ViewBag.UserRoles = userRoles;

        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleAdmin(string id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId == id)
        {
            TempData["Error"] = "Ви не можете змінити роль самому собі!";
            return RedirectToAction("Index");
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAdmin)
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            else
                await _userManager.AddToRoleAsync(user, "Admin");
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId == id)
        {
            TempData["Error"] = "Ви не можете видалити власний акаунт!";
            return RedirectToAction("Index");
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
            await _userManager.DeleteAsync(user);

        return RedirectToAction("Index");
    }
}