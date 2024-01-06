# Blazor Server OpenID Connect

Implements a confidential client using OpenID Connect (code flow with PKCE)

[![.NET](https://github.com/damienbod/BlazorServerOidc/actions/workflows/dotnet.yml/badge.svg)](https://github.com/damienbod/BlazorServerOidc/actions/workflows/dotnet.yml)

[Securing a Blazor Server application using OpenID Connect and security headers](https://damienbod.com/2024/01/03/securing-a-blazor-server-application-using-openid-connect-and-security-headers/)

### Powershell (identity provider project)

Add-Migration "init_sts" -c ApplicationDbContext  

## Running manually

Update-Database -Context ApplicationDbContext

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
> The required security headers cannot be applied to Blazor Web and should not be used in production

https://learn.microsoft.com/en-us/aspnet/core/migration/70-80

