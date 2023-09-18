using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventHub.Application.Dtos.Response.Tickets;

public record TicketViewModel
(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    List<string> Features,
    Guid EventId,
    string EventName
);
