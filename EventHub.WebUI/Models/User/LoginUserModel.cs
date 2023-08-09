namespace EventHub.WebUI.Models.User;

public class LoginUserModel
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; } = false;
    public string ReturnUrl { get; set; } = string.Empty;
}
