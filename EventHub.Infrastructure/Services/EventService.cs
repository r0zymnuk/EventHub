using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Infrastructure.Services;
public class EventService : IEventService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public EventService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public Task<Event> CreateEventAsync(Event @event)
    {
        throw new NotImplementedException();
    }

    public Task<Event> DeleteEventAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public async Task<Event?> GetEventByIdAsync(Guid eventId)
    {
        return await context.Events.Include(e => e.Tickets).Include(e => e.Categories).FirstOrDefaultAsync(e => e.Id == eventId);
    }

    public async Task<IEnumerable<Event>> GetEventsAsync(int take = 12, int skip = 0, string filterString = "")
    {
        var events = context.Events
            .Where(e => e.Start >= DateTime.Now && !e.IsPrivate);

        if (!string.IsNullOrEmpty(filterString))
        {
            var filters = filterString.Split('&');
            foreach (var filter in filters)
            {
                if (filter.Contains("category"))
                {
                    var category = filter.Split('=')[1];
                    events = events.Where(e => e.Categories.Any(c => c.Name == category));
                }
                if (filter.Contains("location"))
                {
                    var location = filter.Split('=')[1];
                    events = events.Where(e => e.Location.City == location);
                }
                if (filter.Contains("format"))
                {
                    var format = filter.Split('=')[1];
                    events = events.Where(e => e.Format.ToString() == format);
                }
                if (filter.Contains("status"))
                {
                    var status = filter.Split('=')[1];
                    events = events.Where(e => e.Status.ToString() == status);
                }
                if (filter.Contains("isFree"))
                {
                    var isFree = filter.Split('=')[1];
                    events = events.Where(e => e.IsFree.ToString().ToLower() == isFree.ToLower());
                }
                if (filter.Contains("isTour"))
                {
                    var isTour = filter.Split('=')[1];
                    events = events.Where(e => e.IsTour.ToString().ToLower() == isTour.ToLower());
                }
            }
        }

        return await events.Skip(skip).Take(take).OrderBy(e => e.CreatedAt).ToListAsync();
    }

    public Task<Event> UpdateEventAsync(Guid eventId, Event @event)
    {
        throw new NotImplementedException();
    }
}
