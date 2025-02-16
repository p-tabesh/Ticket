using Ticket.Domain.Enums;

namespace Ticket.Application.Models;

public class TicketDTO
{
    private int _categoryId;
    public string Subject { get; set; }
    public string Body { get; set; }
        
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public int SubmitedUserId { get; } = 1;
    public int CategoryId
    {
        get => _categoryId;
        set
        {
            if (int.TryParse(value.ToString(), out int parsedId))
            {
                _categoryId = parsedId;
            }
        }
    }
    public Priority Priority { get; set; }
}
