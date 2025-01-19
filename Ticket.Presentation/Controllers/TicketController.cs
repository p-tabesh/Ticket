using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;
using RedLockNet;
using Ticket.Domain.Enums;
using System.Text.Json;

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
    public async Task<IActionResult> AddTicket([FromBody] TicketDTO ticketDTO)
    {
        using var _lock = await _lockFactory.CreateLockAsync("add-ticket-lock", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        if (_lock.IsAcquired)
        {
            _logger.LogInformation("locked");
            _ticketService.AddTicket(ticketDTO);
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
    public IActionResult GetTickets([FromRoute]int? id)
    {
        if (id == null)
        {
            var tickets = _ticketService.GetAllTickets();
            return Json(tickets);
        }
        var ticket = _ticketService.GetTicket(id.Value);
        return Json(ticket);
    }

    [HttpGet]
    [Route("ticket")]
    public IActionResult GetSpecififStateTickets([FromQuery]Status status)
    {
        if (status == null)
        {
            return BadRequest("invalid status");
        }
        var tickets = _ticketService.GetSpecififStateTickets(status);
        return Json(tickets);
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
