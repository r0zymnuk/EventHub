using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace EventHub.Domain.Entities;
public class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();
    public string ImageUrl { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public Role Role { get; set; } = Role.User;
    public bool IsDisabled { get; set; } = false;

    public ICollection<Category> FavouriteCategories = new Collection<Category>();
    public ICollection<Event> EnteredEvents = new Collection<Event>();
    public ICollection<Ticket> Tickets = new Collection<Ticket>();
    public ICollection<Event> OrganizedEvents = new Collection<Event>();
    public ICollection<Tour> OrganizedTours = new Collection<Tour>();
}
