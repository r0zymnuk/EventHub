﻿namespace EventHub.Domain.Shared;
public class EventBase : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsTour { get; set; } = false;
    public User Organizer { get; set; } = null!;
}
