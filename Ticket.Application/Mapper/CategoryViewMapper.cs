using Ticket.Application.Models.CategoryModels;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class CategoryViewMapper
{
    public static CategoryViewModel MapToDTO(Category category)
    {

        var categoryViewModel = new CategoryViewModel(category.Id,
            category.Title,
            category.Parent?.Title,
            category.DefaultUserAsign.Username);

        return categoryViewModel;
    }
    public static IEnumerable<CategoryViewModel> MapToDTO(IEnumerable<Category> categories)
    {
        var categoryViewModels = new List<CategoryViewModel>();
        foreach (var category in categories)
        {
            if (category == null)
                continue;

            var categoryViewModel = new CategoryViewModel(category.Id,
            category.Title,
            category.Parent?.Title,
            category.DefaultUserAsign.Username);

            categoryViewModels.Add(categoryViewModel);
        }
        return categoryViewModels;
    }
}
