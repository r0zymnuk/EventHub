using IdentityModel.Client;

namespace EventHub.WebUI.Services;

public interface ITokenService{
    Task<TokenResponse> GetTokenAsync(string scope);
}