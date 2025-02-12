namespace Ticket.Domain.Exceptions;


public class CategoryException : Exception
{
    public CategoryException() { }
    public CategoryException(string message)
        : base(message) { }

    public CategoryException(string message, Exception inner)
        : base(message, inner) { }
}
