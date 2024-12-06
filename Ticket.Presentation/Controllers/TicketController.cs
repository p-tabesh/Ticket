using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;

namespace Ticket.Presentation.Controllers;

[Route("ticket")]
public class TicketController : Controller
{
    private TicketService _ticketService;
    public TicketController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddTicket([FromQuery] TicketInfo ticketInfo, CustomerInfo customerInfo)
    {
        _ticketService.AddTicket(ticketInfo, customerInfo);
        return Ok();
    }
}
