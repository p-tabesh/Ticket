namespace Ticket.Domain.IUnitOfWork;

public interface IUnitOfWork
{
    void Commit();
    void Rollback();
}
