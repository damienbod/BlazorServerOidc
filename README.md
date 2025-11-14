# Blazor Server OpenID Connect

Implements a confidential client using OpenID Connect (code flow with PKCE)

[![.NET](https://github.com/damienbod/BlazorServerOidc/actions/workflows/dotnet.yml/badge.svg)](https://github.com/damienbod/BlazorServerOidc/actions/workflows/dotnet.yml)

[Securing a Blazor Server application using OpenID Connect and security headers](https://damienbod.com/2024/01/03/securing-a-blazor-server-application-using-openid-connect-and-security-headers/)

[Migrate ASP.NET Core Blazor Server to Blazor Web](https://damienbod.com/2024/01/15/migrate-asp-net-core-blazor-server-to-blazor-web/)

[Revisiting using a Content Security Policy (CSP) nonce in Blazor](https://damienbod.com/2025/05/26/revisiting-using-a-content-security-policy-csp-nonce-in-blazor/)

## Old blogs

[Using a CSP nonce in Blazor Web](https://damienbod.com/2024/02/19/using-a-csp-nonce-in-blazor-web/)

## Migrations

### Powershell (identity provider project)

Add-Migration "init_sts" -c ApplicationDbContext  

### Running manually

Update-Database -Context ApplicationDbContext

## History

- 2025-11-14 Updated packages. .NET 10
- 2025-05-25 Updated packages, new CSP nonce implementation
- 2025-05-06 Updated packages
- 2024-12-31 Updated packages, .NET 9
- 2024-10-21 Updated packages
- 2024-10-03 Updated packages, updated security headers
- 2024-06-22 Updated packages
- 2024-05-26 Updated packages
- 2024-04-24 Updated packages
- 2024-03-24 Updated packages
- 2024-02-19 Updated packages
- 2024-02-16 Updated packages
- 2024-02-12 Fix CSP, use nonce
- 2024-01-14 Updated packages
- 2024-01-11 Added support for Blazor Web, migrated from Blazor Server

## Links

https://learn.microsoft.com/en-us/aspnet/core/blazor/security/server/

https://learn.microsoft.com/en-us/aspnet/core/blazor/security/server/interactive-server-side-rendering

https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/quick-start-blazor-server-app

https://stackoverflow.com/questions/64853618/oidc-authentication-in-server-side-blazor

https://learn.microsoft.com/en-us/aspnet/core/security/authentication/claims

https://openid.net/developers/how-connect-works/

https://github.com/openiddict/openiddict-core

https://datatracker.ietf.org/doc/html/rfc9126

https://learn.microsoft.com/en-us/aspnet/core/security/authentication/claims

https://stackoverflow.com/questions/59121741/anti-forgery-token-validation-in-mvc-app-with-blazor-server-side-component

## Switch Blazor Server to Blazor Web (Server)

> [!WARNING]  
> The required security headers can only be applied to Blazor Web in InteractiveServer mode

https://github.com/javiercn/BlazorWebNonceService

https://learn.microsoft.com/en-us/aspnet/core/migration/70-80

https://learn.microsoft.com/en-us/aspnet/core/blazor/hosting-models