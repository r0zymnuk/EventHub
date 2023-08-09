using AutoMapper;
using EventHub.Application.Dtos.Response.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<User?> GetUserAsync()
    {
        return await userManager.GetUserAsync(signInManager.Context.User);
    }

    public async Task<AuthResponse> LoginAsync(string username, string password, bool isPersistent)
    {
        var response = new AuthResponse();

        var user = await context.Users.FirstOrDefaultAsync(
            u => u.Email == username || u.PhoneNumber == username || u.UserName == username);
        if (user == null)
        {
            response.Error = "Bad credentials";
            return response;
        }

        SignInResult result;
        try
        {
            result = await signInManager.PasswordSignInAsync(user, password, isPersistent, false);
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

    public async Task<AuthResponse> RegisterAsync(User newUser, string password)
    {
        var response = new AuthResponse();

        bool isTaken = await context.Users.AnyAsync(u => u.Email == newUser.Email || u.PhoneNumber == newUser.PhoneNumber);
        if (isTaken)
        {
            response.Error = "Email or phone number is already taken";
            return response;
        }

        var user = await userManager.CreateAsync(newUser, password);
        var result = await signInManager.PasswordSignInAsync(newUser, password, true, false);
        
        if (user.Errors.Any() || !user.Succeeded)
        {
            response.Error = "Something went wrong";
            return response;
        }
        else if (!result.Succeeded)
        {
            response.Error = "Something went wrong";
            return response;
        }

        return response;
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