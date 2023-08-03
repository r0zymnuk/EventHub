namespace EventHub.Application.Services;
public interface IEventService
{
    Task<IQueryable<Event>> GetEventsAsync(string? filterString);
    Task<Event> GetEventAsync(Guid eventId);
    Task<Event> CreateEventAsync(Event @event);
    Task<Event> UpdateEventAsync(Guid eventId, Event @event);
    Task<Event> DeleteEventAsync(Guid eventId);
}
