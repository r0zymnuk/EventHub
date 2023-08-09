namespace EventHub.Domain.Entities;
public class Tour : EventBase
{
    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public ICollection<Promo> PromoCodes { get; set; } = new List<Promo>();
}
