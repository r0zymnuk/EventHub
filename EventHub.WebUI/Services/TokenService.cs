using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace EventHub.WebUI.Services;

public class TokenService : ITokenService
{
    private readonly ILogger<TokenService> _logger;
    private readonly IOptions<IdentityServerSettings> _identityServiceSettings;
    private readonly DiscoveryDocumentResponse _discoverDocument;

    public TokenService(ILogger<TokenService> logger, IOptions<IdentityServerSettings> identityServiceSettings)
    {
        _logger = logger;
        _identityServiceSettings = identityServiceSettings;
        
        using var client = new HttpClient();
        _discoverDocument = client.GetDiscoveryDocumentAsync(_identityServiceSettings.Value.DiscoveryUrl).Result;
        if (_discoverDocument.IsError)
        {
            _logger.LogError($"Unable to get discovery document: {_discoverDocument.Error}");
            throw new Exception($"Unable to get discovery document: {_discoverDocument.Exception}");
        }
    }

    public async Task<TokenResponse> GetTokenAsync(string scope)
    {
        using var client = new HttpClient();
        var tokenResponse = await client
            .RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoverDocument.TokenEndpoint,
                ClientId = _identityServiceSettings.Value.ClientId,
                ClientSecret = _identityServiceSettings.Value.ClientSecret,
                Scope = scope
            });

        if (tokenResponse.IsError)
        {
            _logger.LogError($"Unable to get token: {tokenResponse.Error}");
            throw new Exception("Unable to get token", tokenResponse.Exception);
        }
        
        return tokenResponse;
    }
}