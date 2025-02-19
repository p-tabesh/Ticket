using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Ticket.Application.Services;

namespace Ticket.Presentation.Controllers;




[ApiController]
[Route("team")]
public class TeamController : Controller
{
    private TeamService _teamService;
    public TeamController(TeamService teamService) => _teamService = teamService;

    [HttpGet]
    [Route("teams")]
    public IActionResult GetTeams([FromQuery] int? id)
    {

        var teams = _teamService.GetTeams(id);
        return Json(teams);
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
        return Json(teams);
    }
}
