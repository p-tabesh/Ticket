using Ticket.Domain.Entity;

namespace Ticket.Application.Models;

public class CategoryFieldsModel
{
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public List<Field> Fields { get; set; }
}
