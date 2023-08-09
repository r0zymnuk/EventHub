using EventHub.Application.Services;
using EventHub.Infrastructure.Data;
using EventHub.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastruction(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddDbContext<ApplicationDbContext>(options =>{
            options.UseSqlServer(connectionString);
        });

        services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        return services;
    }
}
