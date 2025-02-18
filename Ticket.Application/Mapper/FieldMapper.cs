using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class FieldMapper
{
    public static FieldModel MapToDto(Field field)
    {
        var fieldModel = new FieldModel()
        {
            Id = field.Id,
            FieldType = field.Type.ToString(),
            IsRequired = field.IsRequired,
            Name = field.Name,
        };
        return fieldModel;
    }
}
