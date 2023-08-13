namespace EventHub.Application.Dtos.Request.Account;

public record RegisterUserModel(
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string Password,
    string ReturnUrl = ""
);