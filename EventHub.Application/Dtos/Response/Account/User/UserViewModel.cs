using EventHub.Application.Dtos.Response.Event;

namespace EventHub.Application.Dtos.Response.Account.User;

public class UserViewModel
{
    public string ImageUrl { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }

    public List<Category> FavouriteCategories = new();
    public List<EventCardModel> EnteredEvents = new();
    public List<Ticket> Tickets = new();
    public List<EventCardModel> OrganizedEvents = new();
    public List<Tour> OrganizedTours = new();
}
