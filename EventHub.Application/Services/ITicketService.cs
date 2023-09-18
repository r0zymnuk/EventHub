using EventHub.Application.Dtos.Response.Tickets;

namespace EventHub.Application.Services;
public interface ITicketService
{
    Task<List<Ticket>> GetTicketsAsync(Guid eventId);
    Task<TicketViewModel> GetTicketAsync(Guid ticketId);
    Task<Ticket> StartTicketSaleAsync(Guid eventId, Ticket ticket);
    Task<TicketPurchaseResult> PurchaseTicketAsync(Guid eventId, Guid ticketId, int quantity);
    Task<bool> UseTicketAsync(Guid ticketId);
    Task<Event> CancelTicketAsync(Guid eventId, Guid ticketId, int quantity);
    Task<Event> UpdateTicketAsync(Guid eventId, Guid ticketId, Ticket ticket);
}
