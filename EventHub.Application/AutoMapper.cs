using EventHub.Application.Dtos.Request;
using EventHub.Application.Dtos.Response;
using Microsoft.Extensions.Options;

namespace EventHub.Application;
public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AddEventDto, Event>();
        CreateMap<Event, GetEventDto>().AfterMap((src, dest) =>
        {
            dest.Location = new Location(src.Country, src.City, src.Address, src.Latitude, src.Longitude);
        });

        CreateMap<AddTourDto, Tour>();
    }
}
