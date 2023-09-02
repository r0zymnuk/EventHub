using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, string imageFolderPath)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IImageService, ImageService>(_ => new ImageService(imageFolderPath));
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        //services.AddDefaultIdentity<User>(options =>
        //    {
        //        options.Password.RequireNonAlphanumeric = false;
        //    })
        //    .AddEntityFrameworkStores<ApplicationDbContext>()
        //    .AddDefaultUI()
        //    .AddDefaultTokenProviders();


        return services;
    }
}
