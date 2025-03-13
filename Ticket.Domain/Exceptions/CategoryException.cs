namespace Ticket.Domain.Exceptions;

public enum ErrorType
{
    NotFound,
    AddError,
    EditError,
    RemoveError,
    ValidationError
}
public class BaseCustomException : Exception
{
    public ErrorType ErrorType { get; }
    public BaseCustomException(ErrorType errorType) 
        : base()
    {
        ErrorType = errorType;
    }
    public BaseCustomException(ErrorType errorType, string message)
        : base(message)
    {
        ErrorType = errorType;
    }

    public BaseCustomException(ErrorType errorType, string message, Exception inner)
        : base(message, inner)
    {
        ErrorType = errorType;
    }
}

