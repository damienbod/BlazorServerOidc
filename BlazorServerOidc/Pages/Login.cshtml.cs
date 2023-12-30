using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorServerOidc.Pages;

/// <summary>
/// Original src:
/// https://github.com/dotnet/blazor-samples/blob/main/8.0/BlazorWebOidc/BlazorWebOidc/LoginLogoutEndpointRouteBuilderExtensions.cs
/// </summary>
public class LoginModel : PageModel
{
    public async Task OnGet(string redirectUri)
    {
        await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme,
            GetAuthProperties(redirectUri));
    }

    private static AuthenticationProperties GetAuthProperties(string? returnUrl)
    {
        const string pathBase = "/";

        // Prevent open redirects.
        if (string.IsNullOrEmpty(returnUrl))
        {
            returnUrl = pathBase;
        }
        else if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
        {
            returnUrl = new Uri(returnUrl, UriKind.Absolute).PathAndQuery;
        }
        else if (returnUrl[0] != '/')
        {
            returnUrl = $"{pathBase}{returnUrl}";
        }

        return new AuthenticationProperties { RedirectUri = returnUrl };
    }
}
