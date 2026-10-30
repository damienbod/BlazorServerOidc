using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace BlazorWebApp;

public static class LoginLogoutEndpoints
{
    public static WebApplication MapLoginLogoutEndpoints(this WebApplication app)
    {
        app.MapGet("/login", async context =>
        {
            var returnUrl = GetLocalReturnUrl(context.Request.Query["returnUrl"]);

            await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = returnUrl
            });
        }).AllowAnonymous();

        app.MapPost("/logout", async (
            HttpContext context,
            [FromForm(Name = "ReturnUrl")] string? returnUrl) =>
        {
            if (context.User.Identity?.IsAuthenticated ?? false)
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            }
            else
            {
                context.Response.Redirect("/");
            }
        });

        return app;
    }

    private static string GetLocalReturnUrl(StringValues returnUrl)
    {
        var value = returnUrl.ToString();

        if (string.IsNullOrEmpty(value)
            || value[0] != '/'
            || value.StartsWith("//", StringComparison.Ordinal)
            || value.StartsWith("/\\", StringComparison.Ordinal)
            || value.Contains('\\')
            || value.Any(char.IsControl))
        {
            return "/";
        }

        return value;
    }
}
