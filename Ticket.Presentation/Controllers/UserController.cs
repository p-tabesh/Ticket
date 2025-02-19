using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;

namespace Ticket.Presentation.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService) => _userService = userService;


        [Authorize(CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("add-user")]
        public IActionResult AddUser([FromBody] UserModel userModel)
        {
            _userService.AddUser(userModel);
            return Ok("User Added Success");
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUser([FromQuery] int? id)
        {
            if (id != null)
            {
                var user = _userService.GetUser(id.Value);
                if (user != null)
                    return Ok(user);
                return Ok("user not found");
            }
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("change-username")]
        public IActionResult ChangeUsername(int userId, string newUsername)
        {
            _userService.ChangeUsername(userId, newUsername);
            return Ok();
        }

        [HttpPost]
        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return Ok();
        }

        [HttpPost]
        [Route("active")]
        public IActionResult ActiveUser(int userId)
        {
            _userService.ActiveUser(userId);
            return Ok();
        }

        [HttpPost]
        [Route("deActive")]
        public IActionResult DeActiveUser(int userId)
        {
            _userService.DeActiveUser(userId);
            return Ok();
        }

        [HttpPost]
        [Route("change-team")]
        public IActionResult ChangeTeam(int newTeamId, int userId)
        {
            _userService.ChangeTeam(newTeamId, userId);
            return Ok();
        }

    }
}
