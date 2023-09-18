namespace EventHub.Application.Dtos.Response.Tickets;
public record TicketPurchaseResult(
    Guid EventId,
    Guid TicketId,
    Guid UserId,
    int Quantity = 0,
    string Message = "Something went wrong",
    bool Success = false
);