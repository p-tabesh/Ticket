namespace Ticket.Infrastructure.ContextStorage;

internal class CurrentContextSuppressor<T> : IDisposable where T : class, ICallContextItem
{
    private T? _savedScope;
    private bool _disposed;

    public CurrentContextSuppressor()
    {
        _savedScope = CallContextStorage<T>.GetCurrentContext();

        CallContextStorage<T>.HideContext();
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_savedScope != null)
        {
            CallContextStorage<T>.SetContext(_savedScope);
            _savedScope = null;
        }

        _disposed = true;
    }
}