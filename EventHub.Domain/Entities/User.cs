using System.Collections.ObjectModel;

namespace EventHub.Domain.Entities;
public class User : BaseClass
{
    public string Picture { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Roles Role { get; set; } = Roles.User;
    public IEnumerable<Category> categories = new Collection<Category>();
    public IEnumerable<Event> events = new Collection<Event>();
}
