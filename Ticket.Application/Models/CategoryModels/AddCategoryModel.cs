namespace Ticket.Application.Models;

public class AddCategoryModel
{
    public string Title { get; set; }
    public int? ParentCategory { get; set; }
    public int DefaultAssigneUserId { get; set; }
}


