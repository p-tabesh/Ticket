namespace Ticket.Application.Models.CategoryModels;

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string? CategoryParentName { get; set; }
    public string DefaultUserAssigneName { get; set; }
}
