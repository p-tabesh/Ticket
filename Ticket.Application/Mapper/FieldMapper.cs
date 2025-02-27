using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class FieldMapper
{
    public static FieldViewModel MapToDto(Field field)
    {
        var fieldModel = new FieldViewModel()
        {
            Id = field.Id,
            FieldType = field.Type.ToString(),
            IsRequired = field.IsRequired,
            Name = field.Name,
        };
        return fieldModel;
    }
}
