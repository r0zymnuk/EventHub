using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Dtos.Response;
using EventHub.Application.Dtos.Response.Account.User;
using EventHub.Application.Dtos.Response.Event;
using EventHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EventHub.Application;
public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Event, GetEventDto>();
        CreateMap<Event, EventModel>()
        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => GetImageUrl(src.ImageUrl)));
        CreateMap<Event, EventCardModel>()
        .ConvertUsing(src => CardFromEvent(src));
        CreateMap<RegisterUserModel, User>();
        CreateMap<User, UserViewModel>();
    }
    public static EventCardModel CardFromEvent(Event @event)
    {
        var description = @event.Description.Length > 150 ? @event.Description[..150] + "..." : @event.Description;
        var imageUrl = GetImageUrl(@event.ImageUrl);
        return new EventCardModel(
        @event.Id,
        @event.Title,
        description,
        imageUrl,
            @event.Start,
            @event.Location.ToString()
        );
    }

    public static string GetImageUrl(string imageUrl)
    {
        return string.IsNullOrWhiteSpace(imageUrl) ? "https://via.placeholder.com/600x400?text=No%20Image" : (imageUrl.Contains(':') ? imageUrl : $"/image/get/{imageUrl}");
    }
}
