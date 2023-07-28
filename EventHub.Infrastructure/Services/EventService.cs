using AutoMapper;
using EventHub.Application;
using EventHub.Application.Dtos.Request;
using EventHub.Application.Services;
using EventHub.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Infrastructure.Services;
public class EventService : IEventService
{
    private readonly DatabaseContext context;
    private readonly IMapper mapper;

    public EventService(DatabaseContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await context.Events.ToListAsync();
    }

    public Task<IEnumerable<Event>> GetEventsByCategoryAsync(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<Event> AddEvent(AddEventDto eventToAdd)
    {
        var mappedEvent = mapper.Map<Event>(eventToAdd);
        await context.Events.AddAsync(mappedEvent);
        await context.SaveChangesAsync();
        return mappedEvent;
    }

    public Task DeleteEventAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
