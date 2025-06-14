﻿using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Models.TicketModels;
using Ticket.Application.Services;
using Ticket.Presentation.Extentions;


namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("ticket")]
public class TicketController : BaseController
{
    private readonly TicketService _ticketService;

    public TicketController(TicketService ticketService) => _ticketService = ticketService;

    [HttpPost]
    [Route("add")]
    public IActionResult AddTicket([FromBody] AddTicketModel ticketDTO)
    {
        _ticketService.AddTicket(ticketDTO, RequestUserId);
        return Ok();
    }

    [HttpPut]
    [Route("edit-ticket")]
    public IActionResult EditTicket([FromBody] AddTicketModel ticketDTO)
    {
        return Ok();
    }
    [HttpPost]
    [Route("add-note")]
    public IActionResult AddTicketNote(AddTicketNoteModel model)
    {
        _ticketService.AddNote(model, RequestUserId);
        return Ok();
    }

    [HttpGet]
    [Route("tickets")]
    public IActionResult GetAllTicket()
    {
        var tickets = _ticketService.GetAllTickets();
        return Ok(tickets);
    }

    [HttpGet]
    [Route("tickets/{id}")]
    public IActionResult GetTicket([FromRoute] int id)
    {
        var ticket = _ticketService.GetTicket(id);
        return Ok(ticket);
    }

    [HttpGet]
    [Route("get-filtered-tickets")]
    public IActionResult GetTicketWithFilter([FromQuery] TicketFilterDataModel ticketFilterDTO)
    {
        var filteredTickets = _ticketService.GetTicketWithFilter(ticketFilterDTO);
        return Ok(filteredTickets);
    }

    [HttpPost]
    [Route("close-ticket")]
    public IActionResult CloseTicket(CloseTicketModel model)
    {
        _ticketService.FinishTicket(model, RequestUserId);
        return Ok();
    }

    [HttpPut]
    [Route("assign-ticket")]
    public IActionResult AssignTicket([FromBody] AssignTicketModel model)
    {
        _ticketService.AssignTicket(model, RequestUserId);
        return Ok();
    }

    [HttpPut]
    [Route("update-status")]
    public IActionResult UpdateStatus([FromBody] UpdateTicketStatusModel model)
    {
        _ticketService.UpdateTicketStatus(model, RequestUserId);
        return base.Ok();
    }
}
