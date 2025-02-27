using Microsoft.AspNetCore.Mvc;
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
    public IActionResult AddTeam([FromBody] string title)
    {
        _teamService.Add(title);
        return Ok();
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult RemoveTeam(int id)
    {
        _teamService.Remove(id);
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
