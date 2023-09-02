using EventHub.Application.Dtos;
using EventHub.Application.Services;
using EventHub.Domain.Entities;
using EventHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventHub.WebUI.Controllers;

[Route("events")]
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

    [HttpGet]
    public async Task<IActionResult> AllEvents([FromQuery] EventFilters? filters)
    {
        var events = await _eventService.GetEventsAsync(filters);
        return View(events);
    }

    [Route("create")]
    [Authorize]
    public async Task<IActionResult> Create()
    {
        if (Request.Method == "POST")
        {
            var @event = new Event();
            await _eventService.CreateEventAsync(@event);
            return RedirectToAction("AllEvents");
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}