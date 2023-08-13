using EventHub.Application.Dtos.Response.Event;
using EventHub.Domain.Entities;
using System.Collections.ObjectModel;

namespace EventHub.Application.Dtos.Response.Account.User;

public class UserViewModel
{
    public string ImageUrl { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public IEnumerable<Category> FavouriteCategories = new Collection<Category>();
    public IEnumerable<EventCardModel> EnteredEvents = new Collection<EventCardModel>();
    public IEnumerable<Ticket> Tickets = new Collection<Ticket>();
    public IEnumerable<EventCardModel> OrganizedEvents = new Collection<EventCardModel>();
    public IEnumerable<Tour> OrganizedTours = new Collection<Tour>();
}
