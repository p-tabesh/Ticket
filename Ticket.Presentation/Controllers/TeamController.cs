﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Mapper;
using Ticket.Application.Models;
using Ticket.Domain.IService;
using Ticket.Presentation.Extentions;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("team")]
public class TeamController : BaseController
{
    private ITeamService _teamService;
    public TeamController(ITeamService teamService) => _teamService = teamService;

    [HttpGet]
    [Route("teams")]
    public IActionResult GetAllTeams()
    {
        var teams = _teamService.GetAllTeams();
        var models = TeamMapper.MapToDto(teams);
        return Ok(models);
    }

    [HttpGet]
    [Route("teams/{id}")]
    public IActionResult GetTeam(int id)
    {
        var team = _teamService.GetTeam(id);
        var model = TeamMapper.MapToDto(team);
        return Ok(model);
    }


    [HttpPost]
    [Route("add")]
    [Authorize(Policy = "Admin")]
    public IActionResult AddTeam([FromBody] AddTeamModel addTeamModel)
    {
        var team = TeamMapper.MapToEntity(addTeamModel);
        _teamService.AddTeam(team);
        return Ok(team.Id);
    }

    [HttpDelete]
    [Route("remove/{id}")]
    [Authorize(Policy = "Admin")]
    public IActionResult RemoveTeam(int id)
    {
        _teamService.RemoveTeam(id);
        return Ok();
    }

    //[HttpGet]
    //[Route("get-users")]
    //public IActionResult GetTeamUsers([FromQuery] int teamId)
    //{
    //    var team = _teamService.GetTeam(teamId);
    //    var teamUsers = TeamMapper.MapToDto(team).Users;
    //    return Ok(teamUsers);
    //}
}
