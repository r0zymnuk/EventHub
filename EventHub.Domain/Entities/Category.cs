namespace EventHub.Domain.Entities;
public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ImageUrl { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Category? ParentCategory { get; set; }

    public IEnumerable<Category> SubCategories = new Collection<Category>();

    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
