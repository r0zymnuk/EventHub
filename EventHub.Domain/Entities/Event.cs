using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Domain.Entities;

public class Event : EventBase
{
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public Location Location { get; set; } = new Location(string.Empty, string.Empty);
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
