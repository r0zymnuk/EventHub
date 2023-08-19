using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Domain.Entities;
public class Ticket : BaseEntity
{
    [MaxLength(25, ErrorMessage = "Name cannot be longer than 25 characters.")]
    public string Name { get; set; } = string.Empty;
    [MaxLength(120, ErrorMessage = "Description cannot be longer than 120 characters.")]
    public string Description { get; set; } = string.Empty;
    [MaxLength(7, ErrorMessage = "Please put only main features of the ticket.")]
    public ICollection<Feature> Features { get; set; } = new List<Feature>();
    public int Quantity { get; set; }
    public int Sold { get; set; }
    public decimal Price { get; set; } = 0;
    public bool IsFree { get; set; } = false;
}
