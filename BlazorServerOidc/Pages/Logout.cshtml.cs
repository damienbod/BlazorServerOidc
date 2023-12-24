using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorServerOidc.Pages;

[Authorize]
[IgnoreAntiforgeryToken]
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