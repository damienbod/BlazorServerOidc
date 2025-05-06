using BlazorWebApp.Components;
using Microsoft.IdentityModel.Logging;

namespace BlazorWebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddHttpContextAccessor();

        var oidcConfig = builder.Configuration.GetSection("OpenIDConnectSettings");

        builder.Services.AddAuthenticationCore();

        builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-XSRF-TOKEN";
            options.Cookie.Name = "__Host-core-X-XSRF-TOKEN";
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        builder.Services.AddSecurityHeaderPolicies()
            .SetDefaultPolicy(SecurityHeadersDefinitions
            .GetHeaderPolicyCollection(oidcConfig["Authority"],
                builder.Environment.IsDevelopment()));

        builder.Services.AddCascadingAuthenticationState();
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }
        else
        {
            IdentityModelEventSource.ShowPII = true;
            IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }

        app.UseSecurityHeaders();

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapLoginLogoutEndpoints();

        app.Run();
    }
}
