using EventHub.Application.Dtos;
using EventHub.Application.Dtos.Request.Event;
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
    private readonly ICategoryService _categoryService;

    public EventController(IEventService eventService, ICategoryService categoryService)
    {
        _eventService = eventService;
        _categoryService = categoryService;
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
    public async Task<IActionResult> Create(CreateEventModel createEvent)
    {
        if (Request.Method == "POST")
        {
            await _eventService.CreateEventAsync(createEvent);
            return RedirectToAction("AllEvents");
        }

        var categories = await _categoryService.GetCategoriesAsync();
        ViewBag.Categories = categories;

        return View();
    }

    [Route("delete/{eventId:guid}")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(Guid eventId)
    {
        await _eventService.DeleteEventAsync(eventId);
        return RedirectToAction("AllEvents");
    }

    [Route("edit/{eventId:guid}")]
    [Authorize]
    public async Task<IActionResult> Edit(Guid eventId, EditEventModel editEvent)
    {
        if (Request.Method == "POST")
        {
            await _eventService.UpdateEventAsync(eventId, editEvent);
            return RedirectToAction("AllEvents");
        }

        var @event = await _eventService.GetEventByIdAsync(eventId);
        if (@event == null)
        {
            return NotFound($"Event with id {eventId} not found.");
        }

        var categories = await _categoryService.GetCategoriesAsync();
        ViewBag.Categories = categories;

        return View(@event);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}