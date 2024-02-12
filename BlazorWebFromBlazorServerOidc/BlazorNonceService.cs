using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace BlazorWebFromBlazorServerOidc;

public class BlazorNonceService : CircuitHandler, IDisposable
{
    private readonly PersistentComponentState _state;
    private readonly PersistingComponentStateSubscription _subscription;

    public BlazorNonceService(PersistentComponentState state)
    {
        if (state.TryTakeFromJson("nonce", out string? nonce))
        {
            if (nonce is not null)
            {
                Nonce = nonce;
            }
            else
            {
                throw new InvalidOperationException("Nonce can't be null when provided");
            }
        }
        else
        {
            _subscription = state.RegisterOnPersisting(PersistNonce);
        }

        _state = state;
    }

    public string? Nonce { get; set; }

    private Task PersistNonce()
    {
        _state.PersistAsJson("nonce", Nonce);
        return Task.CompletedTask;
    }

    public void SetNonce(string nonce)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nonce);

        if (Nonce != null)
        {
            throw new InvalidOperationException("Nonce already defined");
        }

        Nonce = nonce;
    }

    public void Dispose() => ((IDisposable)_subscription)?.Dispose();
}
