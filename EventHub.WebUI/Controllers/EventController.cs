using EventHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.Controllers;

[Route("event")]
public class EventController : Controller
{
    private IEventService _eventService;

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
}