using System.Security.Claims;
using EventHub.Application.Services;
using EventHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.ViewComponents;

public class MenuViewComponent : ViewComponent
{
    private readonly IAccountService _accountService;

    public MenuViewComponent(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _accountService.GetUserAsync();
        return View(user);
    }
}
