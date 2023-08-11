using EventHub.Application.Dtos.Response.Account;
using Microsoft.AspNetCore.Identity;

namespace EventHub.Application.Services;
public interface IAccountService
{
    Task<AuthResponse> RegisterAsync(User newUser, string password);
    Task<AuthResponse> LoginAsync(string email, string password, bool isPersistent);
    Task<User?> GetUserAsync();
    Task<User> UpdateUserAsync(User user);
    Task<User> DeleteUserAsync();
    Task LogoutAsync();
}
