using Ticket.Application.Models.CategoryModels;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class CategoryViewMapper
{
    public static CategoryViewModel MapToDTO(Category category)
    {
        var categoryViewModel = new CategoryViewModel()
        {
            CategoryId = category.Id,
            CategoryName = category.Title,
            CategoryParentName = category.Parent?.Title,
            DefaultUserAssigneName = category.DefaultUserAsign.Username
        };
        return categoryViewModel;
    }
}
