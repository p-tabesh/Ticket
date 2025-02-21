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


        //[Authorize(CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("add-user")]
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
            return Json(users);
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
        public IActionResult ChangePassword(int userId, string newPassword)
        {
            _userService.ChangePassword(userId, newPassword);
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
