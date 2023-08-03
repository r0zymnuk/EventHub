using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Infrastructure.Services;
public class TicketService : ITicketService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public TicketService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public Task<Event> CancelTicketAsync(Guid eventId, Guid ticketId, int quantity, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> GetTicketAsync(Guid eventId, Guid ticketId)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Ticket>> GetTicketsAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    public Task<Event> PurchaseTicketAsync(Guid eventId, Guid ticketId, int quantity, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> StartTicketSaleAsync(Guid eventId, Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task<Event> UpdateTicketAsync(Guid eventId, Guid ticketId, Ticket ticket)
    {
        throw new NotImplementedException();
    }
}   
