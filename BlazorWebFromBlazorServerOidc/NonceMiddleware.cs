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
        var success = context.Items.TryGetValue("NETESCAPADES_NONCE", out var nonce);
        if (success && nonce != null)
        {
            blazorNonceService.SetNonce(nonce.ToString()!);
        }
        await _next.Invoke(context);
    }
}
