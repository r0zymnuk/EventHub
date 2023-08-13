using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Services;
using EventHub.Domain.Entities;
using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    public async Task<IActionResult> Account()
    {
        var user = await accountService.GetUserAsync();
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
        var result = await accountService.LoginAsync(model);

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
        var result = await accountService.RegisterAsync(model);
        
        if (result.Result.Succeeded)
        {
            return Redirect(model.ReturnUrl);
        }
        
        return RedirectToAction("Register", "Account", new { returnUrl = model.ReturnUrl, error = result.Error });
    }

    [HttpPost]
    [Authorize]
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
