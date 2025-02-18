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
    public IActionResult GetTeams([FromRoute] int? id)
    {
        var teams = _teamService.GetTeams(id);
        return Json(teams);
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddTeam()
    {
        return Ok();
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult RemoveTeam()
    {
        return Ok();
    }

    [HttpGet]
    [Route("get-users")]
    public IActionResult GetTeamUsers()
    {
        return Ok();
    }


}
