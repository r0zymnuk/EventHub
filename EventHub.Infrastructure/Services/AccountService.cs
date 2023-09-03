using AutoMapper;
using EventHub.Application.Dtos.Request.Account;
using EventHub.Application.Dtos.Response.Account;
using EventHub.Application.Dtos.Response.Account.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventHub.Infrastructure.Services;
public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(
        ApplicationDbContext context,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IImageService imageService)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _imageService = imageService;
    }

    public async Task<UserViewModel?> GetUserAsync()
    {
        var userId = GetUserId();
        if (userId == Guid.Empty)
        {
            return null!;
        }
        var user = await _context.Users
            .Include(u => u.Tickets)
            .Include(u => u.EnteredEvents)
            .FirstOrDefaultAsync(u => u.Id == userId);
        return _mapper.Map<UserViewModel>(user);
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterUserModel registerUser)
    {
        var response = new RegisterResponse();

        bool isTaken = await _context.Users.AnyAsync(u => u.Email == registerUser.Email || (registerUser.PhoneNumber != null && registerUser.PhoneNumber.Length > 5 && u.PhoneNumber == registerUser.PhoneNumber));
        if (isTaken)
        {
            response.Succeeded = false;
            response.Error = "Email or phone number is already taken";
            return response;
        }
        var newUser = _mapper.Map<User>(registerUser);
        newUser.UserName = newUser.FirstName + newUser.LastName + new Random().Next(1000, 9999);

        // var user = await _userManager.CreateAsync(newUser, registerUser.Password);
        // if (!user.Succeeded)
        // {
        //     response.Error = user.Errors.First().Description;
        //     return response;
        // }

        return response;
    }

    public async Task<User> UpdateUserAsync(UpdateUserModel update)
    {
        var userId = GetUserId();
        if (userId == Guid.Empty)
        {
            return null!;
        }
        var user = await _context.Users
            .Include(u => u.Tickets)
            .Include(u => u.EnteredEvents)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return null!;
        }

        if (!string.IsNullOrWhiteSpace(update.FirstName))
        {
            user.FirstName = update.FirstName;
        }
        if (!string.IsNullOrWhiteSpace(update.LastName))
        {
            user.LastName = update.LastName;
        }
        if (!string.IsNullOrWhiteSpace(update.Email))
        {
            user.Email = update.Email;
        }
        if (!string.IsNullOrWhiteSpace(update.PhoneNumber))
        {
            user.PhoneNumber = update.PhoneNumber;
        }
        if (!string.IsNullOrWhiteSpace(update.ImageUrl))
        {
            if (!string.IsNullOrWhiteSpace(user.ImageUrl))
                await _imageService.DeleteImageAsync(user.ImageUrl);
            user.ImageUrl = update.ImageUrl;
        }

        if (update.Gender != user.Gender)
        {
            user.Gender = update.Gender;
        }

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> DeleteUserAsync()
    {
        var userId = GetUserId();
        if (userId == Guid.Empty)
        {
            return null!;
        }
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return null!;
        }
        _context.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public Guid GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return Guid.Empty;
        }
        return Guid.Parse(userId);
    }
}