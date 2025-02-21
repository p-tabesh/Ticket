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
    public static IEnumerable<CategoryViewModel> MapToDTO(IEnumerable<Category> categories)
    {
        var categoryViewModels = new List<CategoryViewModel>();
        foreach (var category in categories)
        {
            if (category == null)
                continue;

            var model = new CategoryViewModel()
            {
                CategoryId = category.Id,
                CategoryName = category.Title,
                CategoryParentName = category.Parent?.Title,
                DefaultUserAssigneName = category.DefaultUserAsign.Username
            };

            categoryViewModels.Add(model);
        }
        return categoryViewModels;
    }
}
