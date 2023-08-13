using EventHub.Application.Dtos.Response.Tickets;

namespace EventHub.Application.Services;
public interface ITicketService
{
    Task<IQueryable<Ticket>> GetTicketsAsync(Guid eventId);
    Task<Ticket> GetTicketAsync(Guid eventId, Guid ticketId);
    Task<Ticket> StartTicketSaleAsync(Guid eventId, Ticket ticket);
    Task<TicketPurchaseResult> PurchaseTicketAsync(Guid eventId, Guid ticketId, int quantity);
    Task<Event> CancelTicketAsync(Guid eventId, Guid ticketId, int quantity);
    Task<Event> UpdateTicketAsync(Guid eventId, Guid ticketId, Ticket ticket);
}
