using EventHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.ViewComponents;

public class HomeCarouselViewComponent : ViewComponent
{
    private readonly IEventService _eventService;

    public HomeCarouselViewComponent(IEventService eventService)
    {
        _eventService = eventService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var events = await _eventService.GetEventsAsync(take: 6);

        return View(events);
    }
}
