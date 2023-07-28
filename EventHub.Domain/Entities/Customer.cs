namespace EventHub.Domain.Entities;
public class Customer : User
{
    public IEnumerable<Category> Categories = new Collection<Category>();
    public IEnumerable<Event> events = new Collection<Event>();
}
