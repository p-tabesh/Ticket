namespace Ticket.Application.Models;

public class ResponseBaseModel
{
    public bool IsSuccess { get; set; } = true;
    //public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "Successful";
}
