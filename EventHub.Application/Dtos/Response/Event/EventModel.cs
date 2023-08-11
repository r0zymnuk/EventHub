namespace EventHub.Application.Dtos.Response.Event;
public record EventModel(
    Guid Id,
    string Title,
    string Description,
    string Location,
    DateTime Start,
    DateTime End,
    string ImageUrl,
    List<string> Categories,
    bool IsFree,
    List<Ticket> Tickets,
    Status Status,
    Format Format,
    int AgeRestriction,
    int Capacity,
    int Registered,
    string Currency,
    DateTime RegistrationStart,
    DateTime RegistrationEnd
);
