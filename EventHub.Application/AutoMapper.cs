using EventHub.Application.Dtos.Request;
using EventHub.Application.Dtos.Response;
using Microsoft.Extensions.Options;

namespace EventHub.Application;
public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AddEventDto, Event>();
        CreateMap<Event, GetEventDto>();

        CreateMap<AddTourDto, Tour>();
    }
}
