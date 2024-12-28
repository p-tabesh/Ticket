using Ticket.Domain.IRepository;

namespace Ticket.Domain.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    
    void Commit();
    void Rollback();
}

//public interface IUnitOfWork<TContext>:
