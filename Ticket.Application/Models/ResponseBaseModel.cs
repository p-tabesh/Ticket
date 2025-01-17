namespace Ticket.Application.Models;

public class ResponseBaseModel
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
}
