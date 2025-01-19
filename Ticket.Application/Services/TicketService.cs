using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Domain.Enums;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;
using System.Resources;
using System.Reflection;
using Ticket.Application.Mapper;

namespace Ticket.Application.Services;

public class TicketService
{
    private TicketDbContext _ticketDbContext;
    public TicketService(TicketDbContext dbContext)
        => _ticketDbContext = dbContext;
    public void AddTicket(TicketDTO ticketDTO)
    {
        var resourceManager = new ResourceManager("Ticket.Application.Resources.CategoryExceptionMessages", Assembly.GetExecutingAssembly());

        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var defaultUserAsignId = UoW.CategoryRepository.GetById(ticketDTO.CategoryId).DefaultUserAsignId;

            var ticket = new TicketMapper().MapToEntity(ticketDTO);

            ticket.AssignTicket(defaultUserAsignId);
            ticket.AddStatusHistory(Status.Open);
            ticket.AddAudit(Domain.Enums.Action.Add, "added", ticketDTO.SubmitedUserId);
            UoW.TicketRepository.Add(ticket);
            UoW.Commit();
        }
    }

    public void AssignTicket(int ticketId, int userId)
    {
        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            
            var ticket = UoW.TicketRepository.GetById(ticketId);

            ticket.AssignTicket(userId);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public void CloseTicket(int ticketId, string responseBody)
    {
        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var ticket = UoW.TicketRepository.GetById(ticketId);
            ticket.CloseTicket(responseBody);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public void AddNote(int ticketId, string note)
    {
        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var ticket = UoW.TicketRepository.GetById(ticketId);
            ticket.AddNote(note);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public TicketViewDTO GetTicket(int ticketId)
    {
        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var ticket = UoW.TicketRepository.GetById(ticketId);
            var ticketData = new TicketMapper().MapToDTO(ticket);
            return ticketData;
        }
    }
    public IEnumerable<TicketViewDTO> GetSpecififStateTickets(Status status)
    {
        var ticketsData = new List<TicketViewDTO>();
        var mapper = new TicketMapper();

        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var tickets = UoW.TicketRepository.GetWithSpecificState(status);
            foreach (var ticket in tickets)
            {
                var ticketDTO = mapper.MapToDTO(ticket);
                ticketsData.Add(ticketDTO);
            }
        }
        return ticketsData;
    }

    public IEnumerable<TicketViewDTO> GetAllTickets()
    {
        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var ticketsData = new List<TicketViewDTO>();
            var mapper = new TicketMapper();
            var tickets = UoW.TicketRepository.GetAll();
            foreach (var ticket in tickets)
            {
                var ticketDTO = mapper.MapToDTO(ticket);
                ticketsData.Add(ticketDTO);
            }
            return ticketsData;
        }
    }
}



