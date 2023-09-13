using AutoMapper;
using EventHub.Application.Dtos;
using EventHub.Application.Dtos.Request.Event;
using EventHub.Application.Dtos.Response.Event;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Infrastructure.Services;

public class EventService : IEventService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;

    public EventService(ApplicationDbContext context, IMapper mapper, IAccountService accountService)
    {
        _context = context;
        _mapper = mapper;
        _accountService = accountService;
    }

    public async Task<Event> CreateEventAsync(CreateEventModel @event)
    {
        var tickets = ParseJsonTickets(@event.TicketsJson);
        if (tickets.Count == 0)
        {
            tickets.Add(new Ticket
            {
                Name = "Free",
                Description = "Default Admission Ticket",
                Quantity = 50,
                Price = 0,
                IsFree = true,
                Features = new List<Feature>()
            });
        }
        var newEvent = new Event
        {
            ImageUrl = @event.ImageUrl ?? "",
            Title = @event.Title,
            Description = string.IsNullOrWhiteSpace(@event.Description) ? "" : @event.Description,
            RegistrationStart = @event.RegistrationStart,
            RegistrationEnd = @event.RegistrationEnd,
            Start = @event.Start,
            End = @event.End,
            Location = new Location(@event.Country, @event.City, @event.Address ?? ""),
            Format = @event.Format,
            IsPrivate = @event.IsPrivate,
            AgeRestriction = @event.AgeRestriction,
            Capacity = @event.Capacity == 0 ? tickets.Select(t => t.Quantity).Sum() : @event.Capacity,
            Currency = @event.Currency,
        };
        var currentUser = await _context.Users.Include(u => u.OrganizedEvents).FirstOrDefaultAsync(u => u.Id == _accountService.GetUserId());
        if (currentUser is null)
        {
            return null!;
        }
        newEvent.Organizer = currentUser;
        using var transaction = _context.Database.BeginTransaction();

        _context.Events.Add(newEvent);
        newEvent.Categories = _context.Categories.Where(c => @event.Categories.Contains(c.Id)).ToList();
        newEvent.Tickets = tickets;
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return newEvent;
    }

    public async Task<Event> DeleteEventAsync(Guid eventId)
    {
        var currentUser = await _context.Users.Include(u => u.OrganizedEvents).FirstOrDefaultAsync(u => u.Id == _accountService.GetUserId());
        if (currentUser is null)
        {
            return null!;
        }
        var @event = await _context.Events.FindAsync(eventId);
        if (@event is null)
        {
            return null!;
        }
        using var transaction = _context.Database.BeginTransaction();
        currentUser.OrganizedEvents.Remove(@event);
        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return @event;
    }

    public async Task<EventModel?> GetEventByIdAsync(Guid eventId)
    {
        var @event = await _context.Events
            .Include(e => e.Tickets)
            .Include(e => e.Categories)
            .FirstOrDefaultAsync(e => e.Id == eventId);
        return _mapper.Map<EventModel>(@event);
    }

    public async Task<List<EventCardModel>> GetEventsAsync(EventFilters? filters = null, int take = 12, int skip = 0)
    {
        var events = _context.Events
            .Where(e => e.Start >= DateTime.Now.AddHours(-DateTime.Now.Hour) && !e.IsPrivate);

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
            .Select(e => _mapper.Map<EventCardModel>(e))
            .ToListAsync();

        return eventCards;
    }

    public Task<Event> UpdateEventAsync(Guid eventId, Event @event)
    {
        throw new NotImplementedException();
    }

    static List<Ticket> ParseJsonTickets(string ticketsJson)
    {
        var tickets = new List<Ticket>();
        if (string.IsNullOrWhiteSpace(ticketsJson))
        {
            return tickets;
        }
        var ticketsInter = JsonConvert.DeserializeObject<List<TicketIntermediate>>(ticketsJson.Trim());
        if (ticketsInter is null)
        {
            return tickets;
        }
        Ticket t;
        foreach (var tInter in ticketsInter)
        {
            t = new()
            {
                Name = tInter.Name,
                Description = tInter.Description,
                Quantity = tInter.Quantity,
                Price = tInter.Price,
                IsFree = tInter.Price == 0,
                Features = new List<Feature>()
            };
            
            foreach (var fString in tInter.Features)
            {
                t.Features.Add(new Feature { Name = fString });
            }

            tickets.Add(t);
        }

        return tickets;
    }

    class TicketIntermediate
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();
        public int Quantity { get; set; }
        public int Sold { get; set; }
        public decimal Price { get; set; } = 0;
    }
}