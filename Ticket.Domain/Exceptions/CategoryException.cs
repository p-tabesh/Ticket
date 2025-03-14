namespace Ticket.Domain.Exceptions;

public enum ErrorType
{
    NotFound,
    AddError,
    EditError,
    RemoveError,
    ValidationError
}
public class BusinessException : Exception
{
    public ErrorType ErrorType { get; }
    public BusinessException(ErrorType errorType) 
        : base()
    {
        ErrorType = errorType;
    }
    public BusinessException(ErrorType errorType, string message)
        : base(message)
    {
        ErrorType = errorType;
    }

    public BusinessException(ErrorType errorType, string message, Exception inner)
        : base(message, inner)
    {
        ErrorType = errorType;
    }
}

