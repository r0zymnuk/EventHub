using EventHub.Application.Services;
using EventHub.Domain.Entities;
using EventHub.WebUI.Models;
using EventHub.WebUI.Models.User;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
    
    [HttpGet]
    public IActionResult Register(string returnUrl = "", string error = "")
    {
        ViewData["Error"] = error;
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserModel model)
    {
        var newUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            UserName = model.Email
        };
        
        var result = await accountService.RegisterAsync(newUser, model.Password);
        
        if (result.Result.Succeeded)
        {
            return Redirect(model.ReturnUrl);
        }
        
        return RedirectToAction("Register", "Account", new { returnUrl = model.ReturnUrl, error = result.Error });
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await accountService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
