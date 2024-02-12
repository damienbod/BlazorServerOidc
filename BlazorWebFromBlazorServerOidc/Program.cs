using BlazorWebFromBlazorServerOidc.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace BlazorWebFromBlazorServerOidc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<CircuitHandler, BlazorNonceService>(sp =>
        sp.GetRequiredService<BlazorNonceService>()));

        builder.Services.AddScoped<BlazorNonceService>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddOpenIdConnect(options =>
        {
            builder.Configuration.GetSection("OpenIDConnectSettings").Bind(options);

            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.ResponseType = OpenIdConnectResponseType.Code;

            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "name"
            };
        });

        builder.Services.AddRazorPages().AddMvcOptions(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddSingleton<WeatherForecastService>();

        builder.Services.AddControllersWithViews(options =>
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

        var app = builder.Build();

        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // Using an unsecure CSP as CSP nonce is not supported in Blazor Web ...
        app.UseSecurityHeaders(
            SecurityHeadersDefinitions.GetHeaderPolicyCollection(app.Environment.IsDevelopment(),
                app.Configuration["OpenIDConnectSettings:Authority"]));

        app.UseMiddleware<NonceMiddleware>();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();

        app.MapRazorPages();
        app.MapControllers();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode().RequireAuthorization();

        app.Run();
    }
}