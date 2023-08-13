using EventHub.Application.Dtos;
using EventHub.Application.Dtos.Response.Event;

namespace EventHub.Application.Services;
public interface IEventService
{
    Task<List<EventCardModel>> GetEventsAsync(EventFilters? filters = null, int take = 12, int skip = 0);
    Task<EventModel?> GetEventByIdAsync(Guid eventId);
    Task<Event> CreateEventAsync(Event @event);
    Task<Event> UpdateEventAsync(Guid eventId, Event @event);
    Task<Event> DeleteEventAsync(Guid eventId);
}
