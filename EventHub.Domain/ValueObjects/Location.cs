namespace EventHub.Domain.ValueObjects;

public class Location
{
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public double Latitude { get; set; } = 0;
    public double Longitude { get; set; } = 0;

    public Location(string country, string city)
    {
        Country = country;
        City = city;
    }

    public Location(string country, string city, string address) : this(country, city)
    {
        Address = address;
    }

    public Location(string country, string city, double latitude, double longitude) : this(country, city)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public Location(string country, string city, string address, double latitude, double longitude) : this(country, city, address)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
