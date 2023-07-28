using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Domain.Entities;

public class Event : EventBase
{
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public double Latitude { get; set; } = 0;
    public double Longitude { get; set; } = 0;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
