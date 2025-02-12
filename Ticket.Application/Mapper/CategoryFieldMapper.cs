using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class CategoryFieldMapper
{
    public static CategoryFieldsModel MapToDTO(Category category)
    {
        var fields = new List<FieldModel>();
        foreach (var field in category.Fields)
        {
            var model = FieldMapper.MapToDto(field);
            fields.Add(model);
        }

        var fieldModel = new CategoryFieldsModel
        {
            CategoryId = category.Id,
            CategoryTitle = category.Title,
            Fields = fields,
        };
        return fieldModel;
    }
}
