using AutoMapper;
using EventHub.Application.Dtos.Response.Event;
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

    public async Task<EventModel?> GetEventByIdAsync(Guid eventId)
    {
        var @event = await context.Events
            .Include(e => e.Tickets)
            .Include(e => e.Categories)
            .FirstOrDefaultAsync(e => e.Id == eventId);
        return mapper.Map<EventModel>(@event);
    }

    public async Task<IEnumerable<EventCardModel>> GetEventsAsync(int take = 12, int skip = 0, string filterString = "")
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
                if (filter.Contains("registrationOpen"))
                {
                    var registrationOpen = filter.Split('=')[1];
                    if (registrationOpen.ToLower() == "true")
                    {
                        events = events.Where(e => e.RegistrationStart >= DateTime.Now && e.RegistrationEnd < DateTime.Now);
                    }
                }
                if (filter.Contains("format"))
                {
                    var format = filter.Split('=')[1];
                    events = events.Where(e => e.Format.ToString() == format);
                }
                if (filter.Contains("location"))
                {
                    var location = filter.Split('=')[1];
                    events = events.Where(e => e.Location.City == location);
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
                if (filter.Contains("orderBy"))
                {
                    var orderBy = filter.Split('=')[1];
                    if (orderBy.ToLower() == "date")
                    {
                        events = events.OrderBy(e => e.Start);
                    }
                    if (orderBy.ToLower() == "newest")
                    {
                        events = events.OrderByDescending(e => e.CreatedAt);
                    }
                    if (orderBy.ToLower() == "price")
                    {
                        events = events.Include(e => e.Tickets).OrderBy(e => e.Tickets.Min(t => t.Price));
                    }
                }
            }
        }

        var eventCards = await events
            .Skip(skip)
            .Take(take)
            .Select(e => mapper.Map<EventCardModel>(e))
            .ToListAsync();

        return eventCards;
    }

    public Task<Event> UpdateEventAsync(Guid eventId, Event @event)
    {
        throw new NotImplementedException();
    }
}
