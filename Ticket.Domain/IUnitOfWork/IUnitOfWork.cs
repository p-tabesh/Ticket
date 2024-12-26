using Ticket.Domain.IRepository;

namespace Ticket.Domain.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepositoy<T> GetGenericRepositoy<T>() where T: class;
    void Commit();
    void Rollback();
}

//public interface IUnitOfWork<TContext>:
