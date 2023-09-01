using AutoMapper;
using EventHub.Application.Dtos;
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

    public async Task<List<EventCardModel>> GetEventsAsync(EventFilters? filters = null, int take = 12, int skip = 0)
    {
        var events = context.Events
            .Where(e => e.Start >= DateTime.Now && !e.IsPrivate);

        if (filters is not null)
        {
            if (!string.IsNullOrWhiteSpace(filters.Search))
            {
                events = events.Where(e => e.Title.Contains(filters.Search) || e.Description.Contains(filters.Search));
            }
            if (filters.Start is not null)
            {
                if (filters.End is not null)
                {
                    events = events
                        .Where(e => e.Start >= filters.Start
                                               && e.End <= filters.End.Value.AddDays(1));
                }
                else
                {
                    events = events
                        .Where(e => e.Start >= filters.Start);
                }
            }
            else
            {
                if (filters.End is not null)
                {
                    events = events.Where(e => e.End <= filters.End.Value.AddDays(1));
                }
            }
            if (!string.IsNullOrWhiteSpace(filters.Category))
            {
                events = events.Where(e => e.Categories.Any(c => c.Name == filters.Category));
            }
            if (filters.RegistrationOpen is not null)
            {
                if (filters.RegistrationOpen == true)
                {
                    events = events.Where(e => e.RegistrationStart >= DateTime.Now && DateTime.Now < e.RegistrationEnd);
                }
            }
            if (filters.Format is not null)
            {
                events = events.Where(e => e.Format == filters.Format);
            }
            if (!string.IsNullOrWhiteSpace(filters.Country))
            {
                events = events.Where(e => e.Location.Country == filters.Country);
            }
            if (!string.IsNullOrWhiteSpace(filters.City))
            {
                events = events.Where(e => e.Location.City == filters.City);
            }
            if (filters.Status is not null)
            {
                events = events.Where(e => e.Status == filters.Status);
            }
            if (filters.IsFree is not null && filters.IsFree == true)
            {
                events = events.Where(e => e.Tickets.Any(t => t.IsFree));
            }
            if (filters.IsTour is not null)
            {
                events = events.Where(e => e.IsTour == filters.IsTour);
            }
            if (!string.IsNullOrWhiteSpace(filters.OrderBy))
            {
                if (filters.Desc is not null)
                {
                    if (filters.OrderBy.ToLower() == "date")
                    {
                        events = events.OrderByDescending(e => e.Start);
                    }
                    if (filters.OrderBy.ToLower() == "newest")
                    {
                        events = events.OrderByDescending(e => e.CreatedAt);
                    }
                    if (filters.OrderBy.ToLower() == "price")
                    {
                        events = events.Include(e => e.Tickets).OrderByDescending(e => e.Tickets.Min(t => t.Price));
                    }
                }
                else
                {
                    if (filters.OrderBy.ToLower() == "date")
                    {
                        events = events.OrderBy(e => e.Start);
                    }
                    if (filters.OrderBy.ToLower() == "newest")
                    {
                        events = events.OrderBy(e => e.CreatedAt);
                    }
                    if (filters.OrderBy.ToLower() == "price")
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