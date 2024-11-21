using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Ticket.Domain.Entity;

public class Category
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public Category? Parent { get; private set; }
    public int? ParentId { get; private set; }
    public ICollection<Category> ChildCategories { get; private set; }
    public User DefaultUserAsign { get; private set; }
    public int? DefaultUserAsignId { get; private set; }
    public ICollection<Field>? Fields { get; private set; }
    public ICollection<Tickets>? Tickets { get; private set; }
    public Category()
    {
        Fields = new List<Field>();
    }

    public Category(string title, int? parentId, User defaultUserAsign)
    {
        if (string.IsNullOrEmpty(title))
            throw new Exception("Title cannot be empty");
        
        Title = title;
        ParentId = parentId;
        DefaultUserAsign = defaultUserAsign;
    }

    public void AddField(string fieldName, Enum.FieldType fieldType, bool isRequired)
    {
        Fields ??= new List<Field>();

        var field = new Field(fieldName, fieldType, isRequired);
        Fields.Add(field);
    }

    public void UpdateDefaultUserAssinge(User defaultUserAsign)
    {
        if (DefaultUserAsign == defaultUserAsign)
            throw new Exception("user already is default for this category");
        DefaultUserAsign = defaultUserAsign;
    }
}
