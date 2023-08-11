namespace EventHub.Application.Dtos.Response.Event;

public record EventCardModel
(
    Guid Id,
    string Title,
    string Description,
    string ImageUrl,
    DateTime Start,
    string Location
);