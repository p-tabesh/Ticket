using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;
using Ticket.Domain.Exceptions;
using Prometheus;
using Microsoft.Extensions.Caching.Distributed;
using AutoMapper;
using Ticket.Domain.Entity;
using RedLockNet;

namespace Ticket.Presentation.Controllers;

[Route("ticket")]
public class TicketController : Controller
{
    private readonly TicketService _ticketService;
    private readonly ILogger<TicketController> _logger;
    private readonly IDistributedLockFactory _lockFactory;
    //private readonly IDistributedCache _distributedCache;
    public TicketController(TicketService ticketService, ILogger<TicketController> logger/*, IDistributedCache distributedCache*/, IDistributedLockFactory lockFactory)
    {
        //_distributedCache = distributedCache;
        _lockFactory = lockFactory;
        _logger = logger;
        _ticketService = ticketService;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddTicket([FromBody] TicketModel ticketModel)
    {
        using var _lock = await _lockFactory.CreateLockAsync("add-ticket-lock", TimeSpan.FromSeconds(1),TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        if (_lock.IsAcquired)
        {
            _logger.LogInformation("locked");
            _ticketService.AddTicket(ticketModel);
            return Ok();
        }
        return BadRequest("cant use locked object");
    }
}
