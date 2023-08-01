using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace EventHub.Domain.Entities;
public class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();
    public string ImageUrl { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Role RolePass { get; set; } = Role.User;

    public IEnumerable<Category> FavouriteCategories = new Collection<Category>();
    public IEnumerable<Event> EnteredEvents = new Collection<Event>();
    public IEnumerable<Event> OrganizedEvents = new Collection<Event>();
    public IEnumerable<Tour> OrganizedTours = new Collection<Tour>();
}
