using Ticket.Domain.Entity;
using Ticket.Domain.Enums;

namespace Ticket.Domain.IRepository;

public interface ITicketRepository : ICrudRepository<Entity.Ticket> 
{
    IEnumerable<Entity.Ticket> GetWithFilters(DateTime? startDate, DateTime? endDate, int? categoryId, Status? status, Priority? priority);
}
