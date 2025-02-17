using Ticket.Domain.Enums;
using Ticket.Domain.Exceptions;

namespace Ticket.Domain.Entity;

public class Category
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public Category? Parent { get; private set; }
    public int? ParentId { get; private set; }
    public ICollection<Category> ChildCategories { get; private set; }
    public User DefaultUserAsign { get; private set; }
    public int DefaultUserAsignId { get; private set; }
    public ICollection<Field>? Fields { get; private set; }
    public ICollection<Tickets>? Tickets { get; private set; }
    private Category() { }

    public Category(string title, int? parentId, User defaultUserAsign)
    {
        if (string.IsNullOrEmpty(title))
            throw new Exception("Title cannot be empty");

        var categories = new List<Category>();
        if (categories.Any(c => c.Title == title))
            throw new ArgumentException("category already exists");

        if (categories.All(c => c.Id == parentId))
            throw new Exception("Parent category doesnt exists");

        Title = title;
        ParentId = parentId;
        DefaultUserAsign = defaultUserAsign;
    }

    public override string ToString()
    {
        return Title;
    }

    public void AddField(string fieldName, FieldType fieldType, bool isRequired)
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

    public List<Field> GetFields()
    {
        Fields ??= new List<Field>();
        var fields = this.Fields.ToList();
        return fields;
    }

    public void RemoveField(int fieldId)
    {
        Fields ??= new List<Field>();

        if (!Fields.Any(f => f.Id == fieldId))
        {
            throw new Exception("Field doesnt exists");
        }
        var field = Fields.FirstOrDefault(f => f.Id == fieldId);
        Fields.Remove(field);
    }
    public void EditTitle(string title, int categoryId)
    {
        if (string.IsNullOrEmpty(title))
            throw new Exception("name is invalid");

        var categories = new List<Category>();

        if (categories.Any(c => c.Id == categoryId))
            throw new CategoryException("category doesnt exists");

        if (categories.All(c => c.Title.Trim() == title.Trim()))
            throw new CategoryException("Another category with this name already exists");
        
        Title = title;
    }
}
