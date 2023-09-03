using System.ComponentModel.DataAnnotations;

namespace EventHub.Application.Dtos.Request.Account;
public record UpdateUserModel(
    [MaxLength(50)]
    string? FirstName,
    [MaxLength(50)]
    string? LastName,
    [MaxLength(50)]
    string? Email,
    [MaxLength(15)]
    string? PhoneNumber,
    Gender Gender,
    string? ImageUrl
);