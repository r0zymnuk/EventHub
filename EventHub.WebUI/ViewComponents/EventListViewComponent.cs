﻿using EventHub.Application.Dtos;
using EventHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.ViewComponents;

public class EventListViewComponent : ViewComponent
{
    private readonly IEventService _eventService;

    public EventListViewComponent(IEventService eventService)
    {
        _eventService = eventService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int take = 12, int skip = 0, EventFilters? filters = null)
    {
        var events = await _eventService
            .GetEventsAsync(filters, take, skip);
        return View(events.ToList());
    }
}
