using System.ComponentModel.DataAnnotations;

namespace EventHub.Domain.ValueObjects;

public class Feature
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(75, ErrorMessage = "Please put only main features of the ticket.")]
    public string Name { get; set; } = string.Empty;
}