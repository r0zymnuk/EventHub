using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Services;
using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace EventHub.WebUI.Controllers;
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        this._accountService = accountService;
    }

    [Authorize]
    public async Task<IActionResult> Account()
    {
        var user = await _accountService.GetUserAsync();
        return View(user);
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = "")
    {
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = returnUrl
        });
    }

    [HttpGet]
    public IActionResult Register(string returnUrl = "/")
    {
        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "oidc");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        return await Task.Run(() => SignOut(new AuthenticationProperties { RedirectUri = "/" },
            "cookie", "oidc"));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateAccount(UpdateUserModel model)
    {
        var user = await _accountService.UpdateUserAsync(model);
        if (user is null)
        {
            TempData["messageJson"] = JsonSerializer
                .Serialize(new MessageViewModel("Something went wrong", MessageType.Error));
        }
        else
        {
            // string message = "Successfully updated: ";
            // if (!string.IsNullOrWhiteSpace(model.FirstName))
            // {
            //     message += $"First Name, ";
            // }
            // if (!string.IsNullOrWhiteSpace(model.LastName))
            // {
            //     message += $"Last Name, ";
            // }
            // if (!string.IsNullOrWhiteSpace(model.Email))
            // {
            //     message += $"Email, ";
            // }
            // if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
            // {
            //     message += $"Phone Number, ";
            // }
            // if (!string.IsNullOrWhiteSpace(model.ImageUrl))
            // {
            //     message += $"Image Url, ";
            // }
            // message = message.Trim().TrimEnd(',', ':');
            string message = "Successfully updated";
            TempData["messageJson"] = JsonSerializer
                .Serialize(new MessageViewModel(message, MessageType.Success));
        }
        return RedirectToAction("Account", "Account");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteAccount()
    {
        await _accountService.DeleteUserAsync();
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
