using Microsoft.EntityFrameworkCore;
using Ticket.Application.Extentions;
using Ticket.Infrastructure.Context;
using Ticket.Test;


var admin = "admin@".ToSha256();
Console.WriteLine(admin);

var options = new DbContextOptionsBuilder<TicketDbContext>().UseInMemoryDatabase("TicketTestingDb").Options;
var dbContext = new TicketDbContext(options); 

var test = new UnitTest1(dbContext);
test.Test();