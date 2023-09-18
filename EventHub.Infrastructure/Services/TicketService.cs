using EventHub.Application.Dtos.Response.Tickets;
using Microsoft.EntityFrameworkCore;
using QRCoder;

namespace EventHub.Infrastructure.Services;
public class TicketService : ITicketService
{
    private readonly ApplicationDbContext _context;

    private readonly IAccountService _accountService;
    private readonly IEmailService _emailService;

    //private readonly IMapper mapper;
    public TicketService(
        ApplicationDbContext context,
        //IMapper mapper,
        IAccountService accountService,
        IEmailService emailService)
    {
        _context = context;
        _accountService = accountService;
        //_mapper = mapper;
        _emailService = emailService;
    }

    public Task<Event> CancelTicketAsync(Guid eventId, Guid ticketId, int quantity)
    {
        throw new NotImplementedException();
    }

    public async Task<TicketViewModel> GetTicketAsync(Guid ticketId)
    {
        var @event = await _context.Events
            .Include(e => e.Tickets)
            .FirstOrDefaultAsync(e => e.Tickets.Any(t => t.Id == ticketId));

        if (@event == null)
            return null!;

        var ticket = @event.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
            return null!;

        return new TicketViewModel
        (
            ticket.Id,
            ticket.Name,
            ticket.Description,
            ticket.Price,
            ticket.Quantity,
            ticket.Features.Select(f => f.Name).ToList(),
            @event.Id,
            @event.Title
        );
    }

    public async Task<List<Ticket>> GetTicketsAsync(Guid eventId)
    {
        var @event = await _context.Events
            .Include(e => e.Tickets)
            .FirstOrDefaultAsync(e => e.Id == eventId);

        if (@event == null)
            return new List<Ticket>();

        return @event.Tickets.ToList();
    }

    public async Task<bool> UseTicketAsync(Guid ticketId)
    {
        var userId = _accountService.GetUserId();
        var user = await _context.Users.AsNoTracking()
            .Include(u => u.Tickets).AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return false;

        var ticket = user.Tickets.FirstOrDefault(t => t.Id == ticketId);
        if (ticket == null)
            return false;

        return true;
    }

    public async Task<TicketPurchaseResult> PurchaseTicketAsync(Guid eventId, Guid ticketId, int quantity)
    {
        var userId = _accountService.GetUserId();
        var user = await _context.Users
            .Include(u => u.Tickets)
            .Include(u => u.EnteredEvents)
            .FirstOrDefaultAsync(u => u.Id == userId);

        var result = new TicketPurchaseResult(eventId, ticketId, user!.Id, quantity);

        var @event = _context.Events.Include(e => e.Tickets).FirstOrDefault(e => e.Id == eventId);
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

        await _context.SaveChangesAsync();

        // var qrGenerator = new QRCodeGenerator();

        string emailContent = $"You have successfully purchased {quantity} ticket(s) for event {@event.Title}";
        await _emailService.SendEmailAsync(user.Email!, "Ticket purchase", emailContent);

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
