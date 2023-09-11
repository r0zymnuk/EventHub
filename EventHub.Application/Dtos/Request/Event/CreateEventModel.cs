namespace EventHub.Application.Dtos.Request.Event;
public record CreateEventModel
(
    string ImageUrl,
    string Title,
    string Description,
    DateTime RegistrationStart,
    DateTime RegistrationEnd,
    DateTime Start,
    DateTime End,
    string TicketsJson,
    Format Format,
    bool IsPrivate,
    int AgeRestriction,
    int Capacity,
    string Currency,
    List<Guid> Categories,
    string Country,
    string City,
    string Address
);
