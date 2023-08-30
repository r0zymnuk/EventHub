namespace EventHub.WebUI.Services;

public class IdentityServerSettings
{
    public string DiscoveryUrl { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public bool UseHttps { get; set; }
}