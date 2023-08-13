using EventHub.Application.Dtos.Response.Tickets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Infrastructure.Services;
public class TicketService : ITicketService
{
    private readonly ApplicationDbContext context;
    //private readonly IMapper mapper;
    private readonly SignInManager<User> signInManager;

    public TicketService(
        ApplicationDbContext context, 
        //IMapper mapper,
        SignInManager<User> signInManager)
    {
        this.context = context;
        //this.mapper = mapper;
        this.signInManager = signInManager;
    }

    public Task<Event> CancelTicketAsync(Guid eventId, Guid ticketId, int quantity)
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

    public async Task<TicketPurchaseResult> PurchaseTicketAsync(Guid eventId, Guid ticketId, int quantity)
    {
        var userId = signInManager.UserManager.GetUserId(signInManager.Context.User);
        var user = await context.Users
            .Include(u => u.Tickets)
            .Include(u => u.EnteredEvents)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

        var result = new TicketPurchaseResult(eventId, ticketId, user!.Id, quantity);

        var @event = context.Events.Include(e => e.Tickets).FirstOrDefault(e => e.Id == eventId);
        if (@event == null)
            return result with { Message = $"Event with id: '{eventId}' was not found" };
        
        var ticket = @event.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
            return result with { Message = $"Ticket with id: '{ticketId}' was not found" };

        if (ticket.Quantity < quantity)
            return result with { Message = $"Not enough tickets available. Only {ticket.Quantity} left" };

        /*
            Payment logic goes here
         */

        ticket.Sold += quantity;
        user.Tickets.Add(ticket);
        if (!user.EnteredEvents.Contains(@event))
            user.EnteredEvents.Add(@event);

        await context.SaveChangesAsync();
    
        return result with { Success = true, Message = "Ticket purchased successfully" };
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
