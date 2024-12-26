namespace Ticket.Infrastructure.ContextStorage;

/*
 * This object is being used to identify the context in storage,
 * we could use a unique value (like guid string), but this is
 * cheaper and faster (it is like the implementation of TransactionScope)
*/
internal class CallContextIdentifier : MarshalByRefObject
{
}