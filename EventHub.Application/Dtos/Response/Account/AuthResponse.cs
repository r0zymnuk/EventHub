using Microsoft.AspNetCore.Identity;

namespace EventHub.Application.Dtos.Response.Account;
public class AuthResponse
{
    public SignInResult Result { get; set; } = SignInResult.Failed;
    public string? Error { get; set; }
}
