using Ticket.Domain.Enums;

namespace Ticket.Domain.Entity;

public class Field
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public FieldType Type { get; private set; }
    public bool IsRequired { get; private set; }

    public ICollection<Category> Categories { get; private set; }

    public Field() { }

    public Field(string name, FieldType type, bool isRequired)
    {
        if (String.IsNullOrEmpty(name))
            throw new ArgumentNullException("name");

        var fields = new List<Field>();

        if (fields.Any(f => f.Name.Trim() == name.Trim()))
            throw new Exception("another field with this name already exists");

        Name = name;
        Type = type;
        IsRequired = isRequired;
    }

    public void Edit(string? name, FieldType? type, bool? isRequired)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;
        if (type.HasValue)
            Type = type.Value;
        if (isRequired.HasValue)
            IsRequired = isRequired.Value;
    }
}
