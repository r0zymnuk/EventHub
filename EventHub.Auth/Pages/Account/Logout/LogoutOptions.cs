
namespace IdentityServerHost.Pages.Logout;

public class LogoutOptions
{
#pragma warning disable CA2211 // Non-constant fields should not be visible
    public static bool ShowLogoutPrompt = true;
    public static bool AutomaticRedirectAfterSignOut = false;
#pragma warning restore CA2211 // Non-constant fields should not be visible
}