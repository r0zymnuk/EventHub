namespace EventHub.Application.Services;
public interface IEventService
{
    Task<IEnumerable<Event>> GetEventsAsync(int take = 12, int skip = 0, string filterString = "");
    Task<Event?> GetEventByIdAsync(Guid eventId);
    Task<Event> CreateEventAsync(Event @event);
    Task<Event> UpdateEventAsync(Guid eventId, Event @event);
    Task<Event> DeleteEventAsync(Guid eventId);
}
