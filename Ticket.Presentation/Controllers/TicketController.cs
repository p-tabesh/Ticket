using Microsoft.AspNetCore.Mvc;
using RedLockNet;
using Ticket.Application.Models;
using Ticket.Application.Services;


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
    [Route("add-ticket")]
    public async Task<IActionResult> AddTicket([FromBody] TicketDTO ticketDTO)
    {
        _logger.LogInformation("locked");
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
        var resource = $"ticket:{ticketId}";

        await using (var _lock = await _lockFactory.CreateLockAsync(resource, expiryTime))
        {

            if (_lock.IsAcquired)
            {
                //await Task.Delay(5000);
                return Ok("ticket locked");
            }

            return BadRequest("object is locked");
        }

    }

    [HttpPost]
    [Route("edit-ticket")]
    public IActionResult EditTicket([FromBody] TicketDTO ticketDTO)
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
            return Json(tickets);
        }
        var ticket = _ticketService.GetTicket(id.Value);
        return Json(ticket);
    }

    [HttpPost]
    [Route("get-tickets")]
    public IActionResult GetTicketWithFilter([FromQuery] TicketFilterDTO ticketFilterDTO)
    {
        var filteredTickets = _ticketService.GetTicketWithFilter(ticketFilterDTO);
        return Ok(filteredTickets);
    }

    [HttpPost]
    [Route("close-ticket")]
    public IActionResult CloseTicket(int ticketId, string responseBody)
    {
        _ticketService.CloseTicket(ticketId, responseBody);
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
