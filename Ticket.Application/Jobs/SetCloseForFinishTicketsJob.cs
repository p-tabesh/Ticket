using Coravel.Invocable;
using Ticket.Application.Services;

namespace Ticket.Application.Jobs;

public class SetCloseForFinishTicketsJob : IInvocable
{
    private TicketService _ticketService;
    public SetCloseForFinishTicketsJob(TicketService ticketService)
    {
        _ticketService = ticketService;
    }
    public async Task Invoke()
    {
        await _ticketService.SetCloseForFinishedTickets();
    }
}
