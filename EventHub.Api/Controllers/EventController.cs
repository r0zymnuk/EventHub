using EventHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize]
public class EventController : ControllerBase
{
    //private readonly ILogger<EventController> _logger;
    private readonly IEventService _eventService;

    public EventController(
        //ILogger<EventController> logger, 
        IEventService eventService)
    {
        //_logger = logger;
        _eventService = eventService;
    }

    [HttpGet(Name = "GetEvents")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _eventService.GetEventsAsync(take: 3));
    }
}