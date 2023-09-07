using EventHub.Application.Dtos.Request.Event;

namespace EventHub.Application.Dtos.Request;
public class AddTourDto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ICollection<CreateEventModel> Events { get; set; } = new List<CreateEventModel>();
    public Guid Organizer { get; set; }
}
