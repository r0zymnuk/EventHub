using EventHub.Application.Dtos.Response.Event;

namespace EventHub.Application.Services;
public interface IEventService
{
    Task<IEnumerable<EventCardModel>> GetEventsAsync(int take = 12, int skip = 0, string filterString = "");
    Task<EventModel?> GetEventByIdAsync(Guid eventId);
    Task<Event> CreateEventAsync(Event @event);
    Task<Event> UpdateEventAsync(Guid eventId, Event @event);
    Task<Event> DeleteEventAsync(Guid eventId);
}
