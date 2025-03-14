using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface ICategoryFieldRepository : ICrudRepository<CategoryField>
{
    IEnumerable<CategoryField> GetByCategoryId(int categoryId);
    IEnumerable<CategoryField> GetByFieldId(int fieldId);
    CategoryField GetByCategoryIdAndFieldId(int categoryId, int fieldId);
}
