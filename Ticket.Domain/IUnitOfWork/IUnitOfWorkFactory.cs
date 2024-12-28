namespace Ticket.Domain.IUnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork CreateUnitOfWork();
}
