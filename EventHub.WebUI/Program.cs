using EventHub.Application;
using EventHub.Infrastructure;
using EventHub.Infrastructure.Data;
using EventHub.WebUI.Filters;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection")!,
    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "users"));
builder.Services.AddScoped<MessageActionFilter>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(MessageActionFilter));
})
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/Components/{0}/{1}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/Components/{0}/Default.cshtml");

    })
    .AddDataAnnotationsLocalization()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "uk" };
    options.SetDefaultCulture("en");
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    Seed.SeedData(app);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
