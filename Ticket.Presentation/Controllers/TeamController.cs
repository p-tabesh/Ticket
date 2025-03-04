using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;
using Ticket.Presentation.Extentions;

namespace Ticket.Presentation.Controllers;




[ApiController]
[Route("team")]
public class TeamController : BaseController
{
    private TeamService _teamService;
    public TeamController(TeamService teamService) => _teamService = teamService;

    [HttpGet]
    [Route("teams")]
    public IActionResult GetTeams([FromQuery] int? id)
    {

        var teams = _teamService.GetTeams(id);
        return Ok(teams);
    }

    [HttpPost]
    [Route("add")]
    [Authorize(Policy = "Admin")]
    public IActionResult AddTeam([FromBody] AddTeamModel addTeamModel)
    {
        _teamService.Add(addTeamModel);
        return Ok();
    }

    [HttpPost]
    [Route("remove")]
    [Authorize(Policy = "Admin")]
    public IActionResult RemoveTeam([FromBody] RemoveTeamModel removeTeamModel)
    {
        _teamService.Remove(removeTeamModel);
        return Ok();
    }

    [HttpGet]
    [Route("get-users")]
    public IActionResult GetTeamUsers()
    {
        var teams = _teamService.GetTeams();
        return Ok(teams);
    }
}
