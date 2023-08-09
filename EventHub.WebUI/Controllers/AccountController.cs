using EventHub.Application.Services;
using EventHub.WebUI.Models.User;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.Controllers;
public class AccountController : Controller
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    public async Task<IActionResult> Account()
    {
        var user = await this.accountService.GetUserAsync();
        return View(user);
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = "", string error = "")
    {
        ViewData["Error"] = error;
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUserModel model)
    {
        var result = await accountService.LoginAsync(model.UserName, model.Password, model.RememberMe);

        if (result.Result.Succeeded)
        {
            return Redirect(model.ReturnUrl);
        }

        return RedirectToAction("Login", "Account", new { returnUrl = model.ReturnUrl, error = result.Error });
    }
}
