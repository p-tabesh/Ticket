using Ticket.Application.Models;
using Ticket.Domain.Entity;


namespace Ticket.Application.Mapper;

public static class TicketMapper
{

    public static TicketViewModel MapToDTO(Domain.Entity.Ticket ticket)
    {
        var audits = new List<TicketAuditViewModel>();

        foreach (var audit in ticket.TicketAudit)
        {
            var auditModel = TicketAuditMapper.MapToDTO(audit);
            audits.Add(auditModel);
        }

        var notes = new List<TicketNoteViewModel>();

        foreach (var note in ticket.TicketNote)
        {
            var noteModel = TicketNoteMapper.MapToDTO(note);
            notes.Add(noteModel);
        }

        var model = new TicketViewModel()
        {
            Id = ticket.Id,
            Body = ticket.Body,
            ResponseBody = ticket.ResponseBody,
            CreationDate = ticket.CreationDate,
            Priority = ticket.Priority.ToString(),
            Status = ticket.Status.ToString(),
            Subject = ticket.Subject,
            SubmitedUser = ticket.SubmitUser.Username,
            Category = ticket.Category.Title,
            AssignedUser = ticket.AssignUser.Username,
            NationalCode = ticket.NationalCode,
            PhoneNumber = ticket.PhoneNumber,
            Audit = audits,
            Notes = notes
        };
        return model;
    }

    public static Domain.Entity.Ticket MapToEntity(AddTicketModel ticketModel)
    {
        var ticket = new Domain.Entity.Ticket(
            ticketModel.Subject,
            ticketModel.Body,
            ticketModel.Priority,
            ticketModel.NationalCode,
            ticketModel.PhoneNumber,
            ticketModel.CategoryId,
            ticketModel.SubmitedUserId);
        return ticket;
    }
}