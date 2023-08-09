using AutoMapper;
using EventHub.Application.Dtos.Response.Event;
using EventHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.ViewComponents;

public class EventListViewComponent : ViewComponent
{
    private readonly IEventService _eventService;
    private readonly IMapper mapper;

    public EventListViewComponent(IEventService eventService, IMapper mapper)
    {
        _eventService = eventService;
        this.mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var events = await _eventService.GetEventsAsync();
        List<EventCardModel> eventsModel = new();
        foreach (var e in events)
        {
            eventsModel.Add(EventCardModel.FromEvent(e));
        }
        return View(eventsModel);
    }
}
