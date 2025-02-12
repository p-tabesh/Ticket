using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface ITicketRepository
{
    void Add(Tickets ticket);
}
