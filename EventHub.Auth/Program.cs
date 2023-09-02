using Duende.IdentityServer;
using EventHub.Auth;
using EventHub.Auth.Data;
using EventHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<IdentityServerDbContext>(options =>
    {
        options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
    });

    builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
    }).AddEntityFrameworkStores<IdentityServerDbContext>();

    builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;

        options.EmitStaticAudienceClaim = true;
    })
        .AddConfigurationStore(options =>
        {
            options.ConfigureDbContext = b =>
                b.UseSqlServer(connectionString,
                               sql => sql.MigrationsAssembly(migrationsAssembly));
        })
        .AddOperationalStore(options =>
        {
            options.ConfigureDbContext = b =>
                b.UseSqlServer(connectionString,
                               sql => sql.MigrationsAssembly(migrationsAssembly));
        })
        .AddAspNetIdentity<User>();

    builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "990620830600-9lovef3m4h5gtcjpih9visukfahpve7n.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();
    app.UseRouting();

    app.UseIdentityServer();

    app.UseAuthorization();
    app.MapRazorPages().RequireAuthorization();

    if (args.Contains("/seed"))
    {
        Log.Information("Seeding database...");
        SeedData.EnsureSeedData(app);
        Log.Information("Done seeding database.");
        return;
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}