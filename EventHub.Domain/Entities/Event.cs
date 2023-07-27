namespace EventHub.Domain.Entities;

public class Event : BaseClass
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public Location Location { get; set; } = new Location("Ukraine", "Kyiv");
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
