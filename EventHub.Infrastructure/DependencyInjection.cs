using EventHub.Application.Services;
using EventHub.Infrastructure.Data;
using EventHub.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IImageService, ImageService>(provider =>
        {
            var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            return new ImageService(imageFolderPath);
        });
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddDbContext<ApplicationDbContext>(options =>{
            options.UseSqlServer(connectionString);
        });

        services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        return services;
    }
}
