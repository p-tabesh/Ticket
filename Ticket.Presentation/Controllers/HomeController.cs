using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetTeams(int? id)
    {
        var teams = _teamService.GetTeams();
        return Json(teams);
    }
}
