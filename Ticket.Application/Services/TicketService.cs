using Ticket.Application.Mapper;
using Ticket.Application.Models;
using Ticket.Application.Models.TicketModels;
using Ticket.Domain.Enums;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class TicketService
{
    private TicketDbContext _dbContext;
    public TicketService(TicketDbContext dbContext)
        => _dbContext = dbContext;
    public void AddTicket(AddTicketModel ticketDTO, int submiter)
    {
        //var resourceManager = new ResourceManager("Ticket.Application.Resources.CategoryExceptionMessages", Assembly.GetExecutingAssembly());

        using (var UoW = new UnitOfWork(_dbContext))
        {
            var category = UoW.CategoryRepository.GetById(ticketDTO.CategoryId);
            ticketDTO.SubmitedUserId = submiter;
            var ticket = TicketMapper.MapToEntity(ticketDTO);

            ticket.AssignTicket(category.DefaultUserAsign, submiter);
            ticket.AddStatusHistory(Status.Open, ticketDTO.SubmitedUserId);
            UoW.TicketRepository.Add(ticket);
            UoW.Commit();
        }
    }

    public void AssignTicket(AssignTicketModel model, int assignerUserId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {

            var ticket = UoW.TicketRepository.GetById(model.TicketId);

            if (ticket.AssignUserId == model.UserId)
                throw new Exception("User already has this ticket");

            if (ticket.SubmitUserId == model.UserId)
                throw new Exception("Cannot assign ticket to submiter");
            var user = UoW.UserRepository.GetById(model.UserId);
            ticket.AssignTicket(user, assignerUserId);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public void FinishTicket(CloseTicketModel model, int userId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            if (string.IsNullOrEmpty(model.ResponseBody))
                throw new Exception("Enter valid response");

            var ticket = UoW.TicketRepository.GetById(model.TicketId);

            ticket.FinishTicket(model.ResponseBody, userId);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public void AddNote(AddTicketNoteModel model, int userId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var ticket = UoW.TicketRepository.GetById(model.TicketId);
            ticket.AddNote(model.Note,userId);
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

    public void UpdateTicketStatus(UpdateTicketStatusModel model,int userId)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var ticket = UoW.TicketRepository.GetById(model.TicketId);
        ticket.ChangeStatus(model.Status);
        ticket.AddStatusHistory(model.Status, userId);
        UoW.TicketRepository.Update(ticket);
        UoW.Commit();
    }

    public async Task SetCloseForFinishedTickets()
    {
        using var UoW = new UnitOfWork(_dbContext);
        var tickets = UoW.TicketRepository.GetAll().Where(s => s.Status == Status.Finished);
        foreach (var ticket in tickets)
        {
            ticket.CloseTicket();
            UoW.TicketRepository.Update(ticket);
        }
        UoW.Commit();
    }
}



