using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Services;
using Ticket.Presentation.Models;

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
    public IActionResult AddTicket([FromQuery] TicketModel ticketModel)
    {
        //_ticketService.AddTicket(ticketModel.TicketInfo, ticketModel.CustomerInfo);
        return Ok();
    }
}
