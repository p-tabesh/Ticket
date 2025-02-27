namespace Ticket.Application.Models;

public class CategoryFieldsViewModel
{
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public List<FieldViewModel> Fields { get; set; }
}
