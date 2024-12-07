using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;
using Ticket.Domain.Exceptions;

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
    public IActionResult AddTicket([FromBody] TicketInfo ticketInfo, CustomerInfo customerInfo)
    {
        try
        {
            _ticketService.AddTicket(ticketInfo, customerInfo);
            return Ok();
        }
        catch (CategoryException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest("somthing went wrong");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}
