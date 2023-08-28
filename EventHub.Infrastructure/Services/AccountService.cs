using AutoMapper;
using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Dtos.Response.Account;
using EventHub.Application.Dtos.Response.Account.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Infrastructure.Services;
public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IImageService _imageService;

    public AccountService(
        ApplicationDbContext context,
        IMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IImageService imageService)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _imageService = imageService;
    }

    public async Task<UserViewModel?> GetUserAsync()
    {
        var userId = _signInManager.UserManager.GetUserId(_signInManager.Context.User);
        var user = await _context.Users
            .Include(u => u.Tickets)
            .Include(u => u.EnteredEvents)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginUserModel loginUser)
    {
        var response = new AuthResponse();

        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.Email == loginUser.UserName || u.PhoneNumber == loginUser.UserName || u.UserName == loginUser.UserName);
        if (user == null)
        {
            response.Error = "Bad credentials";
            return response;
        }

        SignInResult result;
        try
        {
            result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RememberMe, false);
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

        bool isTaken = await _context.Users.AnyAsync(u => u.Email == registerUser.Email || (registerUser.PhoneNumber != null && registerUser.PhoneNumber.Length > 5 && u.PhoneNumber == registerUser.PhoneNumber));
        if (isTaken)
        {
            response.Error = "Email or phone number is already taken";
            return response;
        }
        var newUser = _mapper.Map<User>(registerUser);
        newUser.UserName = newUser.FirstName + newUser.LastName + new Random().Next(1000, 9999);

        var user = await _userManager.CreateAsync(newUser, registerUser.Password);
        if (!user.Succeeded)
        {
            response.Error = user.Errors.First().Description;
            return response;
        }

        var result = await _signInManager.PasswordSignInAsync(newUser, registerUser.Password, true, false);
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
        await _signInManager.SignOutAsync();
    }

    public async Task<User> UpdateUserAsync(UpdateUserModel update)
    {
        var userId = _signInManager.UserManager.GetUserId(_signInManager.Context.User);
        var userToUpdate = await _context.Users.FindAsync(Guid.Parse(userId!));
        if (userToUpdate == null)
        {
            return null!;
        }

        if (update is null)
        {
            return userToUpdate;
        }

        if (!string.IsNullOrWhiteSpace(update.FirstName))
        {
            userToUpdate.FirstName = update.FirstName;
        }
        if (!string.IsNullOrWhiteSpace(update.LastName))
        {
            userToUpdate.LastName = update.LastName;
        }
        if (!string.IsNullOrWhiteSpace(update.Email))
        {
            userToUpdate.Email = update.Email;
        }
        if (!string.IsNullOrWhiteSpace(update.PhoneNumber))
        {
            userToUpdate.PhoneNumber = update.PhoneNumber;
        }
        if (!string.IsNullOrWhiteSpace(update.ImageUrl))
        {
            if (!string.IsNullOrWhiteSpace(userToUpdate.ImageUrl))
                await _imageService.DeleteImageAsync(userToUpdate.ImageUrl);
            userToUpdate.ImageUrl = update.ImageUrl;
        }

        await _context.SaveChangesAsync();

        return userToUpdate;
    }

    public Task<User> DeleteUserAsync()
    {
        throw new NotImplementedException();
    }
}