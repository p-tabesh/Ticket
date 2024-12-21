using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;
using Ticket.Domain.Exceptions;
using Prometheus;
using Microsoft.Extensions.Caching.Distributed;

namespace Ticket.Presentation.Controllers;

[Route("ticket")]
public class TicketController : Controller
{
    private readonly TicketService _ticketService;
    private readonly ILogger<TicketController> _logger;
    private readonly IDistributedCache _distributedCache;
    public TicketController(TicketService ticketService, ILogger<TicketController> logger, IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
        _logger = logger;
        _ticketService = ticketService;
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddTicket([FromBody] TicketInfo ticketInfo, CustomerInfo customerInfo)
    {
        try
        {
            _logger.LogInformation("test logger");
            _ticketService.AddTicket(ticketInfo, customerInfo);
            return Ok();
        }
        catch (CategoryException ex)
        {
            
            _logger.LogError(ex.Message);
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
