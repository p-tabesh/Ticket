using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Entity;

public class CategoryField
{
    public int Id { get; private set; }
    public Category Category { get; private set; }
    public int CategoryId { get; private set; }
    public Field Field { get; private set; }
    public int FieldId { get; private set; }
    private CategoryField() { }
    public CategoryField(Category category, Field field)
    {
        Category = category;
        Field = field;
    }
}
