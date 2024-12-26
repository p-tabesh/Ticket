using Microsoft.EntityFrameworkCore;
using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.UnitOfWork;

public class UnitOfWorkFactory
{
    private readonly IDbContextFactory<TicketDbContext> _dbContextFactory;
    public UnitOfWorkFactory(IDbContextFactory<TicketDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public IUnitOfWork Create(bool readOnly = false)
    {
        return new UnitOfWork(_dbContextFactory, readOnly);
    }
}
