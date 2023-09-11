using EventHub.Application.Dtos;
using EventHub.Application.Dtos.Response.Event;
using EventHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.WebUI.ViewComponents;

public class EventCardViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(EventCardModel eventModel, bool controls = false)
    {
        return await Task.Run(() => View(new EventCardViewModel(eventModel, controls)));
    }
}

public record EventCardViewModel
(
    EventCardModel Event,
    bool Controls
);