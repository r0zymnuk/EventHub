using AutoMapper;
using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Dtos.Response.Account;
using EventHub.Application.Dtos.Response.Account.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Infrastructure.Services;
public class AccountService : IAccountService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AccountService(
        ApplicationDbContext context,
        IMapper mapper, 
        UserManager<User> userManager, 
        SignInManager<User> signInManager)
    {
        this.context = context;
        this.mapper = mapper;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<UserViewModel?> GetUserAsync()
    {
        var userId = signInManager.UserManager.GetUserId(signInManager.Context.User);
        var user = await context.Users
            .Include(u => u.Tickets)
            .Include(u => u.EnteredEvents)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        return mapper.Map<UserViewModel>(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginUserModel loginUser)
    {
        var response = new AuthResponse();

        var user = await context.Users.FirstOrDefaultAsync(
            u => u.Email == loginUser.UserName || u.PhoneNumber == loginUser.UserName || u.UserName == loginUser.UserName);
        if (user == null)
        {
            response.Error = "Bad credentials";
            return response;
        }

        SignInResult result;
        try
        {
            result = await signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RememberMe, false);
        }
        catch (ArgumentNullException)
        {
            response.Error = "Bad credentials";
            return response;
        }
        if (result == SignInResult.Success)
        {
            response.Result = result;
            return response;
        }

        return response;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterUserModel registerUser)
    {
        var response = new AuthResponse();

        bool isTaken = await context.Users.AnyAsync(u => u.Email == registerUser.Email || (registerUser.PhoneNumber != null && registerUser.PhoneNumber.Length > 5 && u.PhoneNumber == registerUser.PhoneNumber));
        if (isTaken)
        {
            response.Error = "Email or phone number is already taken";
            return response;
        }
        var newUser = mapper.Map<User>(registerUser);
        newUser.ImageUrl = $"https://ui-avatars.com/api/?name={newUser.FirstName}+{newUser.LastName}&size=256&background=random&color=fff";
        newUser.UserName = newUser.FirstName + newUser.LastName + new Random().Next(1000, 9999);

        var user = await userManager.CreateAsync(newUser, registerUser.Password);
        if (!user.Succeeded)
        {
            response.Error = user.Errors.First().Description;
            return response;
        }

        var result = await signInManager.PasswordSignInAsync(newUser, registerUser.Password, true, false);
        if (!result.Succeeded)
        {
            response.Error = "Something went wrong";
            return response;
        }

        response.Result = result;

        return response;
    }
    
    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public Task<User> UpdateUserAsync(User user)
    {
        var userToUpdate = context.Users.FirstOrDefault(u => u.Id == user.Id)
            ?? throw new ArgumentException("User not found");

        // check which properties are changed and update them

        throw new NotImplementedException();
    }

    public Task<User> DeleteUserAsync()
    {
        throw new NotImplementedException();
    }
}