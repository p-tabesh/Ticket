namespace Ticket.Presentation.Models;

public class CategoryModel
{
    public string Title { get; set; }
    public int? ParentCategory {  get; set; }
    public int UserId { get; set; }
}
