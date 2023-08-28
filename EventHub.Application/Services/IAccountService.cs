using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Dtos.Response.Account;
using EventHub.Application.Dtos.Response.Account.User;
using Microsoft.AspNetCore.Identity;

namespace EventHub.Application.Services;
public interface IAccountService
{
    Task<AuthResponse> RegisterAsync(RegisterUserModel newUser);
    Task<AuthResponse> LoginAsync(LoginUserModel loginUser);
    Task<UserViewModel?> GetUserAsync();
    Task<User> UpdateUserAsync(UpdateUserModel user);
    Task<User> DeleteUserAsync();
    Task LogoutAsync();
}
