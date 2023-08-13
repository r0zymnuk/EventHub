using EventHub.Application;
using EventHub.Infrastructure;
using EventHub.Infrastructure.Data;
using EventHub.Infrastructure.DataContext;
using EventHub.WebUI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastruction(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddScoped<MessageActionFilter>();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
