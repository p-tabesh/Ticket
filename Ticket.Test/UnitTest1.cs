using Microsoft.EntityFrameworkCore;
using Ticket.Application.Services;
using Ticket.Infrastructure.Context;

namespace Ticket.Test;

public class UnitTest1
{
    private TicketDbContext _dbContext;

    public UnitTest1(TicketDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [Fact]
    public void Test()
    {

        var service = new TeamService(_dbContext);
        service.Add("salam");
        _dbContext.SaveChanges();
        var savedData = _dbContext.Category.First(); 
        Assert.NotNull(savedData);
        Assert.Equal("salam",savedData.Title);
    }
}