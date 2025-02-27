using Microsoft.EntityFrameworkCore;
using Ticket.Infrastructure.Context;
using Ticket.Test;


var options = new DbContextOptionsBuilder<TicketDbContext>().UseInMemoryDatabase("TicketTestingDb").Options;
var dbContext = new TicketDbContext(options); 

var test = new UnitTest1(dbContext);
test.Test();