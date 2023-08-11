using System.Globalization;
using EventHub.Application.Dtos.Response;
using EventHub.Application.Dtos.Response.Event;

namespace EventHub.Application;
public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AddEventDto, Event>();
        CreateMap<Event, GetEventDto>();
        CreateMap<Event, EventModel>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.ToString()))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => GetCategoryNames(src.Categories)))
            .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets));
        CreateMap<Event, EventCardModel>()
            .ConvertUsing(src => CardFromEvent(src));
    }

    private static List<string> GetCategoryNames(ICollection<Category> categories)
        => categories.Select(category => category.Name).ToList();

    public static EventCardModel CardFromEvent(Event @event)
    {
        var description = @event.Description.Length > 150 ? @event.Description[..150] + "..." : @event.Description;
        var imageUrl = string.IsNullOrWhiteSpace(@event.ImageUrl) ? "https://via.placeholder.com/600x400?text=No%20Image" : @event.ImageUrl;

        return new EventCardModel(
            @event.Id,
            @event.Title,
            description,
            imageUrl,
            @event.Start,
            @event.Location.ToString()
        );
    }
}
