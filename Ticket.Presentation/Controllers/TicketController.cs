using Microsoft.AspNetCore.Mvc;
using RedLockNet;
using Ticket.Application.Models;
using Ticket.Application.Services;
using Ticket.Presentation.Extentions;


namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("ticket")]
public class TicketController : BaseController
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
    public IActionResult AddTicket([FromBody] AddTicketModel ticketDTO)
    {
        _ticketService.AddTicket(ticketDTO);
        return Ok();
    }

    [HttpPost]
    [Route("start-editing")]
    public async Task<IActionResult> StartEdititing(int ticketId)
    {
        var expiryTime = TimeSpan.FromSeconds(60);
        var waitTime = TimeSpan.FromSeconds(1);
        var retryTime = TimeSpan.FromSeconds(1);
        var resource = $"ticket-{ticketId}";

        await using var _lock = await _lockFactory.CreateLockAsync(resource, expiryTime);
        if (_lock.IsAcquired)
        {
            //await Task.Delay(5000);
            return Ok("ticket locked");
        }
        return BadRequest("object locked");
    }

    [HttpPost]
    [Route("edit-ticket")]
    public IActionResult EditTicket([FromBody] AddTicketModel ticketDTO)
    {
        return Ok();
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
    public IActionResult GetTickets([FromRoute] int? id)
    {
        if (id == null)
        {
            var tickets = _ticketService.GetAllTickets();
            return Ok(tickets);
        }
        var ticket = _ticketService.GetTicket(id.Value);
        return Ok(ticket);
    }

    [HttpPost]
    [Route("get-tickets")]
    public IActionResult GetTicketWithFilter([FromQuery] TicketFilterDataModel ticketFilterDTO)
    {
        var filteredTickets = _ticketService.GetTicketWithFilter(ticketFilterDTO);
        return Ok(filteredTickets);
    }

    [HttpPost]
    [Route("close-ticket")]
    public IActionResult CloseTicket(int ticketId, string responseBody)
    {
        _ticketService.CloseTicket(ticketId, responseBody, UserId);
        return Ok();
    }

    [HttpPut]
    [Route("assign-ticket")]
    public IActionResult AssignTicket(int ticketId, int userId)
    {
        _ticketService.AssignTicket(ticketId, userId);
        return Ok();
    }
}
