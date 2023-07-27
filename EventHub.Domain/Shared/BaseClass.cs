namespace EventHub.Domain.Shared;

public class BaseClass
{
    public Guid Id { get; set; } = Guid.NewGuid();
    DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    DateTime? UpdatedAt { get; set; }
    DateTime? DeletedAt { get; set; }
}