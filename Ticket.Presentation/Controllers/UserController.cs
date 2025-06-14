﻿using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("users/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("change-username")]
        public IActionResult ChangeUsername([FromBody] string newUsername)
        {
            _userService.ChangeUsername(RequestUserId, newUsername);
            return Ok();
        }

        [HttpPost]
        [Route("change-password")]
        public IActionResult ChangePassword([FromBody] string newPassword)
        {
            _userService.ChangePassword(RequestUserId, newPassword);
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

        [HttpPost]
        [Route("demote")]
        [Authorize(Policy = "Admin")]
        public IActionResult DemoteUser([FromBody] DemoteUserModel model)
        {
            _userService.Demote(model);
            return Ok();
        }

        [HttpPost]
        [Route("promote")]
        [Authorize(Policy = "Admin")]
        public IActionResult PromoteUser([FromBody] PromoteUserModel model)
        {
            _userService.PromoteUser(model);
            return Ok();
        }
    }
}
