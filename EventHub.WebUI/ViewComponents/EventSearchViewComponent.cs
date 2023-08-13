using EventHub.Application.Dtos;
using EventHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.ViewComponents;

public class EventSearchViewComponent : ViewComponent
{
    private readonly IEventService eventService;

    public EventSearchViewComponent(IEventService eventService)
    {
        this.eventService = eventService;
    }

    public async Task<IViewComponentResult> InvokeAsync(EventFilters? filtes = null)
    {
        return View(await eventService.GetEventsAsync(filtes));
    }
}