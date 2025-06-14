﻿namespace Ticket.Domain.Entity;

public class TicketNote
{
    public int Id { get; private set; }
    public string Note { get; private set; }
    public int UserId { get; private set; }
    public int TicketId { get; private set; }
    public DateTime CreationDate { get;set; }
    public User User { get; private set; }
    public Ticket Ticket { get; private set; }

    private TicketNote() { }
    public TicketNote(string note, int userId)
    {
        CreationDate = DateTime.UtcNow;
        Note = note;
        UserId = userId;
    }
}
