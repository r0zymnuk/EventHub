using EventHub.Application.Dtos.Request;
using EventHub.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService eventService;

    public EventController(IEventService eventService)
    {
        this.eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await eventService.GetAllEventsAsync();

        return Ok(events);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] AddEventDto addEvent)
    {
        var newEvent = await eventService.AddEvent(addEvent);

        return CreatedAtAction(nameof(CreateEvent), new { id = newEvent.Id }, newEvent);
    }
}
