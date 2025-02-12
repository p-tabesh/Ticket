namespace Ticket.Application.Models;

public class CategoryFieldsModel
{
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public List<FieldModel> Fields { get; set; }
}
