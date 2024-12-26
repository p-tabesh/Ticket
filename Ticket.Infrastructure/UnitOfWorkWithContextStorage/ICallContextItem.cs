namespace Ticket.Infrastructure.ContextStorage;

internal interface ICallContextItem : IDisposable
{
    CallContextIdentifier ContextIdentifier { get; }
}