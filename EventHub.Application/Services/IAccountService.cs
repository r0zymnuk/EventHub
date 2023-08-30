using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Dtos.Response.Account;
using EventHub.Application.Dtos.Response.Account.User;

namespace EventHub.Application.Services;
public interface IAccountService
{
    Guid GetUserId();
    Task<UserViewModel?> GetUserAsync();
    Task<User> UpdateUserAsync(UpdateUserModel user);
    Task<User> DeleteUserAsync();
}
