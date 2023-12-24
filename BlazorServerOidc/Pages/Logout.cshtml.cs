using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorServerOidc.Pages;

[Authorize]
[IgnoreAntiforgeryToken] // TODO remove this and add the protection to the logout form request
public class LogoutModel : PageModel
{
    public void OnGet()
    {
    }

    public IActionResult OnPostAsync([FromForm] string? returnUrl)
    {
        return SignOut(new AuthenticationProperties
        {
            RedirectUri = returnUrl
        },
        CookieAuthenticationDefaults.AuthenticationScheme,
        OpenIdConnectDefaults.AuthenticationScheme);
    }
}