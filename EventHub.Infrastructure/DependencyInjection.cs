using EventHub.Application.Services;
using EventHub.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastruction(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IEventService, EventService>();
        // services.AddDbContext<DatabaseContext>(options =>
        // {
        //     options.UseSqlServer(connectionString,
        //         b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));
        // }, ServiceLifetime.Transient);

        services.AddDbContext<DatabaseContext>(options =>{
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}
