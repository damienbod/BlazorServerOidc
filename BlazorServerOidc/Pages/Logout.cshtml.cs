using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorServerOidc.Pages;

/// <summary>
/// TODO: small improvement 
/// Remove the IgnoreAntiforgeryToken attrbute and 
/// add the protection to the logout form request if using post
/// 
/// It is also possible to use a GET for the logout
/// Care must be taken with open redirect attacks, 
/// only allow relative paths if accepting form parameters.
/// </summary>
[Authorize]
[IgnoreAntiforgeryToken]
public class LogoutModel : PageModel
{
    public IActionResult OnPostAsync()
    {
        return SignOut(new AuthenticationProperties
        {
            RedirectUri = "/SignedOut"
        },
        CookieAuthenticationDefaults.AuthenticationScheme,
        OpenIdConnectDefaults.AuthenticationScheme);
    }
}