using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Domain.Enums;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;
using System.Resources;
using System.Reflection;
using Ticket.Infrastructure.Repository;
using AutoMapper;


namespace Ticket.Application.Services;

public class TicketService
{
    private TicketDbContext _ticketDbContext;
    private IMapper _mapper;
    public TicketService(TicketDbContext dbContext, IMapper mapper)
    {
        _ticketDbContext = dbContext;
        _mapper = mapper;
    }
    public void AddTicket(TicketModel ticketModel)
    {
        var resourceManager = new ResourceManager("Ticket.Application.Resources.CategoryExceptionMessages",Assembly.GetExecutingAssembly());

        using (var UoW = new UnitOfWork<TicketDbContext>(_ticketDbContext))
        {
            var ticketRepository = UoW.GetGenericRepositoy<Tickets>();
            var userRepository = UoW.GetGenericRepositoy<User>();
            var categoryRepository = UoW.GetGenericRepositoy<Category>();
            var category = categoryRepository.GetById(ticketModel.CategoryId);

            var user = userRepository.GetById(ticketModel.UserId);
            var assignUser = category.DefaultUserAsign;

            var ticket = _mapper.Map<Tickets>(ticketModel);
            ticket.AssignTicket(assignUser);
            ticket.AddStatusHistory(Status.Open);
            ticket.AddAudit(Domain.Enums.Action.Add, "added", user);
            ticketRepository.Add(ticket);
            UoW.Commit();
        }
    }
}
    


