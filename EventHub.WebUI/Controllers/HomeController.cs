using EventHub.WebUI.Filters;
using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using EventHub.WebUI.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using IdentityModel;

namespace EventHub.WebUI.Controllers;

[ServiceFilter(typeof(MessageActionFilter))]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer<HomeController> _stringLocalizer;
    private readonly ITokenService _tokenService;

    public HomeController(ILogger<HomeController> logger, 
        IStringLocalizer<HomeController> stringLocalizer, 
        ITokenService tokenService)
    {
        _logger = logger;
        _stringLocalizer = stringLocalizer;
        _tokenService = tokenService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}