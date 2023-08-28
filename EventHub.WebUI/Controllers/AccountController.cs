using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Services;
using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateAccount(UpdateUserModel model)
    {
        var user = await accountService.UpdateUserAsync(model);
        if (user is null)
        {
            TempData["messageJson"] = JsonSerializer
                .Serialize(new MessageViewModel("Something went wrong", MessageType.Error));
        }
        else
        {
            string message = "Successfully updated: ";
            if (!string.IsNullOrWhiteSpace(model.FirstName))
            {
                message += $"First Name, ";
            }
            if (!string.IsNullOrWhiteSpace(model.LastName))
            {
                message += $"Last Name, ";
            }
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                message += $"Email, ";
            }
            if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                message += $"Phone Number, ";
            }
            if (!string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                message += $"Image Url, ";
            }
            TempData["messageJson"] = JsonSerializer
                .Serialize(new MessageViewModel(message, MessageType.Success));
        }
        return RedirectToAction("Account", "Account");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteAccount()
    {
        await accountService.DeleteUserAsync();
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
