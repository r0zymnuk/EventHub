namespace EventHub.Domain.Entities;

public class Event : EventBase
{
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public Location Location { get; set; } = new Location(string.Empty, string.Empty);

    public Status Status { get; set; } = Status.Upcoming;
    public Format Format { get; set; } = Format.OnSite;
    public bool IsPrivate { get; set; } = false;
    public int AgeRestriction { get; set; } = 0;

    public int Capacity { get; set; } = 0;
    public int Registered { get; set; } = 0;
    public string Currency { get; set; } = string.Empty;

    public ICollection<Promo> PromoCodes { get; set; } = new List<Promo>();

    public DateTime RegistrationStart { get; set; }
    public DateTime RegistrationEnd { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
