using EventHub.Application.Services;
using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventHub.WebUI.Controllers;

[Route("event")]
public class EventController : Controller
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    [HttpGet("id/{eventId:guid}")]
    public async Task<IActionResult> Id(Guid eventId)
    {
        var @event = await _eventService.GetEventByIdAsync(eventId);
        if (@event == null)
        {
            return NotFound($"Event with id {eventId} not found.");
        }
        return View(@event);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}