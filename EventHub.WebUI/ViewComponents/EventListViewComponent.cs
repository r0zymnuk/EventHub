using AutoMapper;
using EventHub.Application.Dtos.Response.Event;
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

    public async Task<IViewComponentResult> InvokeAsync(int take = 12, int skip = 0, string filterString = "")
    {
        var events = await _eventService
            .GetEventsAsync(take: take, skip: skip, filterString: filterString);
        return View(events.ToList());
    }
}
