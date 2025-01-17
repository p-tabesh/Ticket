using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;
using RedLockNet;
using Ticket.Domain.Enums;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("ticket")]
public class TicketController : Controller
{
    private readonly TicketService _ticketService;
    private readonly ILogger<TicketController> _logger;
    private readonly IDistributedLockFactory _lockFactory;
    public TicketController(TicketService ticketService, ILogger<TicketController> logger/*, IDistributedCache distributedCache*/, IDistributedLockFactory lockFactory)
    {
        _lockFactory = lockFactory;
        _logger = logger;
        _ticketService = ticketService;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddTicket([FromBody] TicketModel ticketModel)
    {
        using var _lock = await _lockFactory.CreateLockAsync("add-ticket-lock", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        if (_lock.IsAcquired)
        {
            _logger.LogInformation("locked");
            _ticketService.AddTicket(ticketModel);
            return Ok();
        }
        return BadRequest("cant use locked object");
    }

    [HttpPost]
    [Route("add-note")]
    public IActionResult AddTicketNote(int ticketId, string note)
    {
        _ticketService.AddNote(ticketId, note);
        return Ok();
    }

    [HttpGet]
    [Route("tickets/{id?}")]
    public IActionResult Tickets([FromRoute] int? ticketId)
    {
        return Ok();
    }
    [HttpPost]
    [Route("close-ticket")]
    public IActionResult CloseTicket(int ticketId,string responseBody)
    {
        _ticketService.CloseTicket(ticketId, responseBody);
        return Ok();
    }

    [HttpPut]
    [Route("assign")]
    public IActionResult AssignTicket(int ticketId, int userId)
    {
        _ticketService.AssignTicket(ticketId, userId);
        return Ok();
    }
}
