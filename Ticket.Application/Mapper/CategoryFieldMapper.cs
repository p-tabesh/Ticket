using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class CategoryFieldMapper
{
    public static CategoryFieldsViewModel MapToDTO(Category category)
    {
        var fields = new List<FieldViewModel>();
        foreach (var field in category.Fields)
        {
            var model = FieldMapper.MapToDto(field);
            fields.Add(model);
        }

        var fieldModel = new CategoryFieldsViewModel
        {
            CategoryId = category.Id,
            CategoryTitle = category.Title,
            Fields = fields,
        };
        return fieldModel;
    }
}
