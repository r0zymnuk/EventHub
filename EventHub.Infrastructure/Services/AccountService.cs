using AutoMapper;
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

    public AccountService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public Task<User> DeleteUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<User> RegisterAsync(User newUser, string password)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
