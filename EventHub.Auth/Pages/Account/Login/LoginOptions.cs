namespace IdentityServerHost.Pages.Login;

public class LoginOptions
{
#pragma warning disable CA2211 // Non-constant fields should not be visible
    public static bool AllowLocalLogin = true;
    public static bool AllowRememberLogin = true;
    public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
    public static string InvalidCredentialsErrorMessage = "Invalid username or password";
#pragma warning restore CA2211 // Non-constant fields should not be visible
}