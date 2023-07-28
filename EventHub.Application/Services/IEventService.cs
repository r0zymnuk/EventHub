namespace EventHub.Application.Services;
public interface IEventService
{
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<IEnumerable<Event>> GetEventsByCategoryAsync(Guid categoryId);
    Task<Event> AddEvent(AddEventDto eventToAdd);
    Task DeleteEventAsync(Guid id);
}
