//using System.Reflection;
//using System.Resources;
using Ticket.Application.Mapper;
using Ticket.Application.Models;
using Ticket.Domain.Enums;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class TicketService
{
    private TicketDbContext _dbContext;
    public TicketService(TicketDbContext dbContext)
        => _dbContext = dbContext;
    public void AddTicket(AddTicketModel ticketDTO)
    {
        //var resourceManager = new ResourceManager("Ticket.Application.Resources.CategoryExceptionMessages", Assembly.GetExecutingAssembly());

        using (var UoW = new UnitOfWork(_dbContext))
        {
            var category = UoW.CategoryRepository.GetById(ticketDTO.CategoryId);

            var ticket = TicketMapper.MapToEntity(ticketDTO);

            ticket.AssignTicket(category.DefaultUserAsignId);
            ticket.AddStatusHistory(Status.Open,ticketDTO.SubmitedUserId);
            UoW.TicketRepository.Add(ticket);
            UoW.Commit();
        }
    }

    public void AssignTicket(int ticketId, int userId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {

            var ticket = UoW.TicketRepository.GetById(ticketId);

            ticket.AssignTicket(userId);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public void CloseTicket(int ticketId, string responseBody, int userId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var ticket = UoW.TicketRepository.GetById(ticketId);
            ticket.CloseTicket(responseBody, userId);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public void AddNote(int ticketId, string note)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var ticket = UoW.TicketRepository.GetById(ticketId);
            ticket.AddNote(note);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public TicketViewModel GetTicket(int ticketId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var ticket = UoW.TicketRepository.GetById(ticketId);
            var ticketData = TicketMapper.MapToDTO(ticket);
            return ticketData;
        }
    }
    public IEnumerable<TicketViewModel> GetTicketWithFilter(TicketFilterDataModel ticketFilterDTO)
    {
        var ticketsData = new List<TicketViewModel>();


        using (var UoW = new UnitOfWork(_dbContext))
        {
            var tickets = UoW.TicketRepository.GetFilteredTickets(ticketFilterDTO.StartDate, ticketFilterDTO.EndDate, ticketFilterDTO.CategoryId, ticketFilterDTO.Status, ticketFilterDTO.Priority);
            foreach (var ticket in tickets)
            {
                var ticketDTO = TicketMapper.MapToDTO(ticket);
                ticketsData.Add(ticketDTO);
            }
        }
        return ticketsData;
    }

    public IEnumerable<TicketViewModel> GetAllTickets()
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var ticketsData = new List<TicketViewModel>();

            var tickets = UoW.TicketRepository.GetAll();
            foreach (var ticket in tickets)
            {
                var ticketDTO = TicketMapper.MapToDTO(ticket);
                ticketsData.Add(ticketDTO);
            }
            return ticketsData;
        }
    }
}



