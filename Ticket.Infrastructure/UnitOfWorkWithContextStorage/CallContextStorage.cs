using System.Runtime.CompilerServices;

// ReSharper disable StaticMemberInGenericType

namespace Ticket.Infrastructure.ContextStorage;

internal static class CallContextStorage<T> where T : class, ICallContextItem
{
    // WeakTable is used to allow collecting of objects when no one references to it
    private static readonly ConditionalWeakTable<CallContextIdentifier, T> ContextInstances = new();

    // AsyncLocal is being used to keep the current identifier as ambient (active) one. We are not keeping the whole object in this field
    // because it will be handled by ExecutionContext and might have performance penalty. Instead we are keeping just a simple object and 
    // will find the actual one by this identifier
    private static readonly AsyncLocal<CallContextIdentifier?> CurrentContext = new();

    public static void SetContext(T newContext)
    {
        if (newContext == null)
        {
            throw new ArgumentNullException(nameof(newContext));
        }

        var current = CurrentContext.Value;

        if (current == newContext.ContextIdentifier)
        {
            return;
        }

        CurrentContext.Value = newContext.ContextIdentifier;

        // Add new item to the table
        ContextInstances.GetValue(newContext.ContextIdentifier, _ => newContext);
    }

    public static void RemoveContext()
    {
        var current = CurrentContext.Value;
        CurrentContext.Value = null;

        if (current != null)
        {
            ContextInstances.Remove(current);
        }
    }

    public static void HideContext()
    {
        CurrentContext.Value = null;
    }

    public static T? GetCurrentContext()
    {
        var identifier = CurrentContext.Value;
        if (identifier == null)
        {
            return null;
        }

        if (ContextInstances.TryGetValue(identifier, out var context))
        {
            return context;
        }

        // We have an instance identifier in the CurrentContext but no corresponding instance
        // in our ContextInstances table. Most probable case is that someone let go of object 
        // instance without disposing it. But since we use a ConditionalWeakTable to store 
        // instances (holding a weak reference), the GC would be able to collect it.
        System.Diagnostics.Debug.WriteLine("Object released without disposing.");

        return null;
    }
}