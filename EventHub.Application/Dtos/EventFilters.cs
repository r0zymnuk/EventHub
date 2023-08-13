using EventHub.Domain.Enums;

namespace EventHub.Application.Dtos;

public record EventFilters
(
    string? Search,
    DateTime? Start,
    DateTime? End,
    string? Category,
    string? Country,
    string? City,
    bool? RegistrationOpen,
    Format? Format,
    Status? Status,
    bool? IsFree,
    bool? IsTour,
    string? OrderBy,
    bool? Desc
);