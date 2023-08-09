using EventHub.Domain.Entities;
using System.Collections.ObjectModel;

namespace EventHub.WebUI.Models.User;

public class UserViewModel
{
    public string ImageUrl { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public IEnumerable<Category> FavouriteCategories = new Collection<Category>();
    public IEnumerable<Event> EnteredEvents = new Collection<Event>();
    public IEnumerable<Ticket> Tickets = new Collection<Ticket>();
    public IEnumerable<Event> OrganizedEvents = new Collection<Event>();
    public IEnumerable<Tour> OrganizedTours = new Collection<Tour>();
}
