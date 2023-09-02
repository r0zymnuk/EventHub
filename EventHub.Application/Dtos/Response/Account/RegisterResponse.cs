namespace EventHub.Application.Dtos.Response.Account;
public class RegisterResponse
{
    public bool Succeeded { get; set; } = true;
    public string? Error { get; set; }
}
