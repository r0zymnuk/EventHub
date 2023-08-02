using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Domain.Entities;

public class Event : EventBase
{
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public Location Location { get; set; } = new Location(string.Empty, string.Empty);

    public Status Status { get; set; } = Status.Upcoming;
    public Format Format { get; set; } = Format.OnSite;
    public bool IsFree { get; set; } = false;
    public bool IsPrivate { get; set; } = false;
    public int AgeRestriction { get; set; } = 0;

    public int Capacity { get; set; } = 0;
    public int Registered { get; set; } = 0;
    [MaxLength(3)]
    public string Currency { get; set; } = string.Empty;

    public ICollection<Promo> PromoCodes { get; set; } = new List<Promo>();

    public DateTime RegistrationStart { get; set; } = DateTime.MinValue;
    public DateTime RegistrationEnd { get; set; } = DateTime.MinValue;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
