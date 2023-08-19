using Microsoft.EntityFrameworkCore;
using ChanceNET;
using Location = EventHub.Domain.ValueObjects.Location;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EventHub.Infrastructure.Data;
public class Seed
{
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureDeleted();
        context.Database.Migrate();

        if (!context.Categories.Any())
        {
            context.Categories.Add(new Category { Name = "Music", Description = "Immerse yourself in a symphony of rhythms and melodies as our curated music events transport you to unforgettable sonic landscapes." });
            context.Categories.Add(new Category { Name = "Arts", Description = "Embark on a visual and auditory odyssey through a curated collection of arts events that celebrate the harmony of creativity and culture." });
            context.Categories.Add(new Category { Name = "Sports", Description = "Experience the thrill of victory and the camaraderie of competition with our dynamic range of sports events that ignite your passion for athleticism." });
            context.Categories.Add(new Category { Name = "Science & Tech", Description = "Embark on a journey of discovery and innovation with our thought-provoking science and tech events that illuminate the frontiers of knowledge." });
            context.Categories.Add(new Category { Name = "Fashion", Description = "Step onto the runway of style and elegance with our captivating fashion events, where trends come to life and every fabric tells a story of creativity and expression." });
            context.Categories.Add(new Category { Name = "Food & Drink", Description = "Savor the flavors of culinary excellence with our delectable food and drink events, where gourmet experiences and tantalizing tastings await every palate." });
            context.Categories.Add(new Category { Name = "Charity", Description = "Join hands in making a meaningful difference with our heartwarming charity events, where compassion and generosity come together to impact lives and create positive change." });
            context.Categories.Add(new Category { Name = "Health", Description = "Embrace wellness and vitality with our enriching health events, where expert insights, rejuvenating practices, and holistic approaches empower you on your journey to well-being." });
            context.Categories.Add(new Category { Name = "Travel & Outdoor", Description = "Embark on a voyage of exploration and adventure with our invigorating travel and outdoor events, where the world becomes your playground and nature's wonders unfold before your eyes." });
            

            context.SaveChanges();
        }

        if (!context.Events.Any())
        {
            context.Events.AddRange(
                new Event {
                    Title = "Summer Sounds Music Festival",
                    Description = "Experience a weekend of musical delight with our Summer Sounds Music Festival featuring a lineup of top artists and bands.",
                    ImageUrl = "https://wepik.com/api/image/ai/99d71133-1061-4684-bcd4-004f3438c8c5",
                    Organizer = GenerateRandomUser(),
                    Categories = new List<Category> { context.Categories.First(c => c.Name == "Music") },
                    Tickets = new List<Ticket> {
                        new Ticket { Name = "General Admission", Price = 50, Quantity = 100 },
                    },
                    Location = GenerateRandomLocation(),
                    Status = Status.Upcoming,
                    Format = Format.OnSite,
                    AgeRestriction = 15,
                    Capacity = 1000,
                    Registered = 0,
                    Currency = "USD",
                    PromoCodes = new List<Promo>
                    {
                        new Promo { Code = "SUMMER", Discount = 10 },
                    },
                    RegistrationStart = DateTime.UtcNow.AddDays(-30),
                    RegistrationEnd = DateTime.UtcNow.AddDays(20),
                    Start = DateTime.UtcNow.AddDays(30),
                    End = DateTime.UtcNow.AddDays(32),
                },
                new Event
                {
                    Title = "Art in the Park",
                    Description = "Discover the beauty of art in nature with our Art in the Park event featuring a collection of stunning sculptures and paintings.",
                    ImageUrl = "https://th.bing.com/th/id/OIG.1bg8vO6W76xVIi76arS9",
                    Organizer = GenerateRandomUser(),
                    Categories = new List<Category> { context.Categories.First(c => c.Name == "Arts") },
                    Tickets = new List<Ticket>
                    {
                        new Ticket { Name = "General Admission", IsFree = true, Quantity = 100 },
                    },
                    Location = GenerateRandomLocation(),
                    Status = Status.Upcoming,
                    Format = Format.OnSite,
                    AgeRestriction = 0,
                    Capacity = 1000,
                    Registered = 0,
                    Currency = "USD",
                    Start = DateTime.UtcNow.AddDays(30),
                    End = DateTime.UtcNow.AddDays(32),
                },
                new Event {
                    Title = "Summer Sports Camp",
                    Description = "Join us for a week of fun and games at our Summer Sports Camp featuring a variety of sports and activities.",
                    ImageUrl = "https://th.bing.com/th/id/OIG.Nh1C86pytx7kCT4pWIii",
                    Organizer = GenerateRandomUser(),
                    Categories = new List<Category> { context.Categories.First(c => c.Name == "Sports") },
                    Tickets = new List<Ticket>
                    {
                        new Ticket { Name = "General Admission", Price = 50, Quantity = 100 },
                    },
                    Location = GenerateRandomLocation(),
                    Status = Status.Upcoming,
                    Format = Format.OnSite,
                    AgeRestriction = 15,
                    Capacity = 1000,
                    Registered = 0,
                    Currency = "USD",
                    PromoCodes = new List<Promo>
                    {
                        new Promo { Code = "SUMMER", Discount = 10 },
                    },
                    RegistrationStart = DateTime.UtcNow.AddDays(-30),
                    RegistrationEnd = DateTime.UtcNow.AddDays(19),
                    Start = DateTime.UtcNow.AddDays(20),
                    End = DateTime.UtcNow.AddDays(30),
                },
                new Event
                {
                    Title = "Stairway to Microservices Conference",
                    Description = "Explore the latest trends in technology with our Tech Conference featuring a lineup of top speakers and experts.",
                    ImageUrl = "https://th.bing.com/th/id/OIG.RVN0ZSgvdDKTjxbr0sB.",
                    Organizer = GenerateRandomUser(),
                    Categories = new List<Category> { context.Categories.First(c => c.Name == "Science & Tech") },
                    Tickets = new List<Ticket>
                    {
                        new Ticket { Name = "General Admission", Price = 50, Quantity = 100, Features = new List<Feature>() { new Feature() { Name = "Main presentations"}, new Feature() { Name = "Giveaways"} } },
                        new Ticket { Name = "VIP", Price = 100, Quantity = 50, Features = new List<Feature>() { new Feature() { Name = "Personal meetings"}, new Feature() { Name = "Spicy topic presentations"} } },
                    },
                    Location = GenerateRandomLocation(),
                    Status = Status.Upcoming,
                    Format = Format.Hybrid,
                    Start = DateTime.UtcNow.AddDays(20),
                    End = DateTime.UtcNow.AddDays(30),
                    Currency = "EUR",
                    PromoCodes = new List<Promo>
                    {
                        new Promo { Code = "TECH", Discount = 10 },
                    },
                    RegistrationEnd = DateTime.UtcNow.AddDays(19),
                    AgeRestriction = 12,
                    Capacity = 150,
                });

            context.SaveChanges();
        }
    }

    public static User GenerateRandomUser()
    {
        Chance chance = new();

        return new User
        {
            FirstName = chance.FirstName(),
            LastName = chance.LastName(),
            Email = chance.Email(),
            UserName = chance.Email(),
            PhoneNumber = chance.Phone(),
            ImageUrl = chance.Avatar(),
        };
    }

    public static Location GenerateRandomLocation()
    {
        Chance chance = new();
        var country = chance.Country();
        return new Location(chance.Address(), chance.City())
        {
            Country = country.Name,
            CountryCode = country.Alpha2Code,
            Latitude = chance.Location().Latitude,
            Longitude = chance.Location().Longitude,
        };
    }
}
