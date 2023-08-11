namespace EventHub.WebUI.Models.User;

public record RegisterUserModel(
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string Password,
    string ReturnUrl = ""
);