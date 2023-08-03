using EventHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.ViewComponents;

public class MenuViewComponent : ViewComponent
{
    private readonly UserManager<User> _userManager;

    public MenuViewComponent(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        return View(user);
    }
}
