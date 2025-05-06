using NetEscapades.AspNetCore.SecurityHeaders;

namespace BlazorWebFromBlazorServerOidc;

public class NonceMiddleware
{
    private readonly RequestDelegate _next;

    public NonceMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, BlazorNonceService blazorNonceService)
    {
        var nonce = context.GetNonce();

        if (nonce != null)
        {
            blazorNonceService.SetNonce(nonce);
        }
        await _next.Invoke(context);
    }
}
