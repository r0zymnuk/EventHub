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
        CreateMap<Event, EventCardModel>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title.Length > 25 ? src.Title.Substring(0, 25) + "..." : src.Title))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src =>
                    src.Description.Length > 100 ? src.Description.Substring(0, 100) + "..." : src.Description))
            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.ImageUrl)
                        ? "https://via.placeholder.com/600x400?text=No%20Image"
                        : src.ImageUrl))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.ToString()));
    }
}
