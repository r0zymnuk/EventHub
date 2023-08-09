namespace EventHub.Application.Dtos.Response.Event;

public record EventCardModel
(
    Guid Id,
    string Title,
    string Description,
    string ImageUrl,
    DateTime Start,
    string Location
)
{
    public static EventCardModel FromEvent(Domain.Entities.Event @event)
    {
        var description = @event.Description.Length > 150 ? @event.Description.Substring(0, 150) + "..." : @event.Description;
        var imageUrl = string.IsNullOrWhiteSpace(@event.ImageUrl) ? "https://via.placeholder.com/600x400?text=No%20Image" : @event.ImageUrl;
        
        return new EventCardModel(
            @event.Id,
            @event.Title,
            description,
            imageUrl,
            @event.Start,
            @event.Location.ToString()
        );
    }
}