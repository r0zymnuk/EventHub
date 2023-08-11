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

    public async Task<IEnumerable<EventCardModel>> GetEventsAsync(Dictionary<string, string>? filters = null, int take = 12, int skip = 0)
    {
        var events = context.Events
            .Where(e => e.Start >= DateTime.Now && !e.IsPrivate);

        if (filters is not null)
        {
            if (filters.TryGetValue("category", out var category))
            {
                events = events.Where(e => e.Categories.Any(c => c.Name == category));
            }
            if (filters.TryGetValue("registrationOpen", out var registrationOpen))
            {
                if (registrationOpen.ToLower() == "true")
                {
                    events = events.Where(e => e.RegistrationStart >= DateTime.Now && e.RegistrationEnd < DateTime.Now);
                }
            }
            if (filters.TryGetValue("format", out var format))
            {
                events = events.Where(e => e.Format == (Format)Enum.Parse(typeof(Format), format));
            }
            if (filters.TryGetValue("country", out var country))
            {
                events = events.Where(e => e.Location.Country == country);
            }
            if (filters.TryGetValue("city", out var city))
            {
                events = events.Where(e => e.Location.City == city);
            }
            if (filters.TryGetValue("status", out var status))
            {
                events = events.Where(e => e.Status == (Status)Enum.Parse(typeof(Status), status));
            }
            if (filters.TryGetValue("isFree", out var isFree))
            {
                events = events.Where(e => e.IsFree == bool.Parse(isFree));
            }
            if (filters.TryGetValue("isTour", out var isTour))
            {
                events = events.Where(e => e.IsTour == bool.Parse(isTour));
            }
            if (filters.TryGetValue("orderBy", out var orderBy))
            {
                if (filters.TryGetValue("desc", out var desc))
                {
                    if (orderBy.ToLower() == "date")
                    {
                        events = events.OrderByDescending(e => e.Start);
                    }
                    if (orderBy.ToLower() == "newest")
                    {
                        events = events.OrderByDescending(e => e.CreatedAt);
                    }
                    if (orderBy.ToLower() == "price")
                    {
                        events = events.Include(e => e.Tickets).OrderByDescending(e => e.Tickets.Min(t => t.Price));
                    }
                }
                else
                {
                    if (orderBy.ToLower() == "date")
                    {
                        events = events.OrderBy(e => e.Start);
                    }
                    if (orderBy.ToLower() == "newest")
                    {
                        events = events.OrderBy(e => e.CreatedAt);
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