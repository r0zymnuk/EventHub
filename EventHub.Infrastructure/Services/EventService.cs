﻿using AutoMapper;

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

    public Task<Event> GetEventAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Event>> GetEventsAsync(string? filterString)
    {
        throw new NotImplementedException();
    }

    public Task<Event> UpdateEventAsync(Guid eventId, Event @event)
    {
        throw new NotImplementedException();
    }
}
