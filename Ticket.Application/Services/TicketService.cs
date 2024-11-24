using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Enum;
using Ticket.Domain.IRepository;


namespace Ticket.Application.Services;

public class TicketService
{
    private ITicketRepository _ticketRepository;
    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public void AddTicket()
    {

    }
}
