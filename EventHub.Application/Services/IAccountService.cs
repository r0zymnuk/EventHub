namespace EventHub.Application.Services;
public interface IAccountService
{
    Task<User> RegisterAsync(User newUser, string password);
    Task<User> LoginAsync(string email, string password);
    Task<User> GetUserAsync();
    Task<User> UpdateUserAsync(User user);
    Task<User> DeleteUserAsync();
}
