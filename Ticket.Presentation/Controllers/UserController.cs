using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Services;
using Ticket.Presentation.Extentions;

namespace Ticket.Presentation.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController(UserService userService) => _userService = userService;

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [Route("add-user")]
        [Authorize(Policy = "Admin")]
        public IActionResult AddUser([FromBody] UserModel userModel)
        {
            _userService.AddUser(userModel);
            return Ok();
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetUser([FromQuery] int? id)
        {
            IEnumerable<UserViewModel> users;

            if (!id.HasValue)
                users = _userService.GetUsers();
            else
                users = _userService.GetUsers(id.Value);
            return Ok(users);
        }

        [HttpPost]
        [Route("change-username")]
        public IActionResult ChangeUsername(string newUsername)
        {
            _userService.ChangeUsername(UserId, newUsername);
            return Ok();
        }

        [HttpPost]
        [Route("change-password")]
        public IActionResult ChangePassword(string newPassword)
        {
            _userService.ChangePassword(UserId, newPassword);
            return Ok();
        }

        [HttpPost]
        [Route("active")]
        [Authorize(Policy = "Admin")]
        public IActionResult ActiveUser(int userId)
        {
            _userService.ActiveUser(userId);
            return Ok();
        }

        [HttpPost]
        [Route("deActive")]
        [Authorize(Policy = "Admin")]
        public IActionResult DeActiveUser(int userId)
        {
            _userService.DeActiveUser(userId);
            return Ok();
        }

        [HttpPost]
        [Route("change-team")]
        [Authorize(Policy = "Admin")]
        public IActionResult ChangeTeam(int newTeamId, int userId)
        {
            _userService.ChangeTeam(newTeamId, userId);
            return Ok();
        }
    }
}
